using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MoldCom
{
    // ── ClientDate ───────────────────────────────────────────────────────────
    public class ClientDate
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Adresa { get; set; }
    }

    public class Client : Form
    {
        private TabControl tabMain;
        private TabPage tabMagazin, tabCont;

        // Shop tab
        private ListView lvProducts;
        private TextBox txtSearch;
        private NumericUpDown nudQuantity;
        private Label lblWelcome, lblFeedback, lblStockBadge,
                             lblPriceBig, lblDescText, lblCartCount;
        private List<CartItem> shoppingCart = new List<CartItem>();

        // Account tab
        private TextBox txtNume, txtPrenume, txtTelefon, txtEmail, txtAdresa;
        private TextBox txtParolaVeche, txtParolaNouA, txtParolaConfirm;
        private Label lblStatusDate, lblStatusParola;

        private USER currentUser;

        // ── Design system (matches AdminPan) ────────────────────────────────
        private static readonly Color C_BgBase = Color.FromArgb(18, 24, 32);
        private static readonly Color C_BgSurface = Color.FromArgb(26, 34, 46);
        private static readonly Color C_BgPanel = Color.FromArgb(34, 44, 58);
        private static readonly Color C_BgCard = Color.FromArgb(42, 54, 70);
        private static readonly Color C_BgInput = Color.FromArgb(22, 30, 42);
        private static readonly Color C_Border = Color.FromArgb(52, 68, 88);
        private static readonly Color C_TextPrim = Color.FromArgb(230, 236, 244);
        private static readonly Color C_TextSec = Color.FromArgb(140, 160, 180);
        private static readonly Color C_AccBlue = Color.FromArgb(56, 132, 220);
        private static readonly Color C_AccGreen = Color.FromArgb(48, 168, 108);
        private static readonly Color C_AccRed = Color.FromArgb(196, 64, 72);
        private static readonly Color C_AccAmber = Color.FromArgb(204, 140, 40);

        private static readonly Font F_Logo = new Font("Segoe UI", 11f, FontStyle.Bold);
        private static readonly Font F_SubHead = new Font("Segoe UI", 10f, FontStyle.Bold);
        private static readonly Font F_Body = new Font("Segoe UI", 9f);
        private static readonly Font F_Small = new Font("Segoe UI", 8f);
        private static readonly Font F_Tiny = new Font("Segoe UI", 7.5f);
        private static readonly Font F_PriceBig = new Font("Segoe UI", 22f, FontStyle.Bold);

        private const int PAD = 20;
        private const int GAP = 10;
        private const int HEADER_H = 56;

        // ─────────────────────────────────────────────────────────────────────
        public Client(USER user = null)
        {
            currentUser = user;
            InitializeComponent();
            LoadProducts();
            if (currentUser != null)
            {
                lblWelcome.Text = $"Welcome,  {currentUser.Username}";
                IncarcaDateCont();
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        private void InitializeComponent()
        {
            this.Text = "MoldCom — Online Store";
            this.Size = new Size(1100, 720);
            this.MinimumSize = new Size(980, 640);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = C_BgPanel;

            // ── Top header ───────────────────────────────────────────────
            Panel pnlHeader = new Panel
            {
                BackColor = C_BgBase,
                Dock = DockStyle.Top,
                Height = HEADER_H
            };

            // Blue accent line at very top
            Panel topLine = new Panel { BackColor = C_AccBlue, Dock = DockStyle.Top, Height = 3 };

            Label lblTitle = new Label
            {
                Text = "MOLDCOM",
                ForeColor = C_TextPrim,
                Font = F_Logo,
                Location = new Point(PAD, 18),
                AutoSize = true
            };

            Label lblStoreSub = new Label
            {
                Text = "ONLINE STORE",
                ForeColor = C_AccBlue,
                Font = new Font("Segoe UI", 7f, FontStyle.Bold),
                Location = new Point(PAD, 36),
                AutoSize = true
            };

            lblWelcome = new Label
            {
                Text = "Welcome, Guest",
                ForeColor = C_TextSec,
                Font = F_Small,
                AutoSize = true
            };

            lblCartCount = new Label
            {
                Text = "🛒  0 items",
                ForeColor = C_AccBlue,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                AutoSize = true
            };

            Button btnLogout = MakeHeaderBtn("Logout", C_AccRed);
            btnLogout.Click += (s, e) => this.Close();

            // Anchor welcome + cart on resize
            pnlHeader.Resize += (s, e) =>
            {
                lblWelcome.Location = new Point(pnlHeader.Width / 2 - lblWelcome.Width / 2, 17);
                lblCartCount.Location = new Point(pnlHeader.Width - 200, 20);
                btnLogout.Location = new Point(pnlHeader.Width - 100, 13);
            };

            pnlHeader.Controls.AddRange(new Control[] {
                topLine, lblTitle, lblStoreSub, lblWelcome, lblCartCount, btnLogout });

            // ── Tab control ──────────────────────────────────────────────
            tabMain = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9f)
            };

            tabMagazin = new TabPage { Text = "  🛒  Shop  ", BackColor = C_BgPanel, Padding = new Padding(0) };
            tabCont = new TabPage { Text = "  ⚙   My Account  ", BackColor = C_BgPanel, Padding = new Padding(0) };

            tabMain.TabPages.AddRange(new TabPage[] { tabMagazin, tabCont });

            BuildTabShop();
            BuildTabAccount();

            this.Controls.Add(tabMain);
            this.Controls.Add(pnlHeader);
        }

        // ═══════════════════════════════════════════════════════════════════
        //  TAB 1 — SHOP
        // ═══════════════════════════════════════════════════════════════════
        private void BuildTabShop()
        {
            // ── Top search bar ───────────────────────────────────────────
            Panel pnlSearch = new Panel
            {
                BackColor = C_BgSurface,
                Dock = DockStyle.Top,
                Height = 52
            };
            Panel searchBottom = new Panel { BackColor = C_Border, Dock = DockStyle.Bottom, Height = 1 };

            txtSearch = new TextBox
            {
                Location = new Point(PAD, 13),
                Width = 280,
                BackColor = C_BgInput,
                ForeColor = C_TextSec,
                BorderStyle = BorderStyle.FixedSingle,
                Font = F_Body
            };
            txtSearch.Text = "Search by name or category…";
            txtSearch.GotFocus += (s, e) => { if (txtSearch.ForeColor == C_TextSec) { txtSearch.Text = ""; txtSearch.ForeColor = C_TextPrim; } };
            txtSearch.LostFocus += (s, e) => { if (string.IsNullOrEmpty(txtSearch.Text)) { txtSearch.Text = "Search by name or category…"; txtSearch.ForeColor = C_TextSec; } };
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) DoSearch(); };

            Button btnSearch = MakeBtn("Search", C_AccBlue, 310, 13, 80, 26);
            Button btnReset = MakeBtn("Reset", C_BgInput, 398, 13, 70, 26);
            btnSearch.Click += (s, e) => DoSearch();
            btnReset.Click += (s, e) => { txtSearch.Text = "Search by name or category…"; txtSearch.ForeColor = C_TextSec; LoadProducts(); };

            lblFeedback = new Label
            {
                Text = "",
                ForeColor = C_AccGreen,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                Location = new Point(480, 17),
                AutoSize = true
            };

            pnlSearch.Controls.AddRange(new Control[] { searchBottom, txtSearch, btnSearch, btnReset, lblFeedback });

            // ── Two-column layout: list | details ────────────────────────
            Panel pnlBody = new Panel
            {
                BackColor = C_BgPanel,
                Dock = DockStyle.Fill
            };

            // Product list — left column
            lvProducts = new ListView
            {
                BackColor = C_BgSurface,
                ForeColor = C_TextPrim,
                Font = F_Body,
                View = View.Details,
                FullRowSelect = true,
                GridLines = false,
                BorderStyle = BorderStyle.None,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom
            };
            lvProducts.Columns.AddRange(new ColumnHeader[] {
                new ColumnHeader { Text = "Product",  Width = 200 },
                new ColumnHeader { Text = "Category", Width = 120 },
                new ColumnHeader { Text = "Price",    Width = 110 }
            });
            lvProducts.SelectedIndexChanged += LvProducts_SelectedIndexChanged;

            // Right detail panel
            Panel pnlRight = new Panel
            {
                BackColor = C_BgSurface,
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom
            };

            // Size both panels on resize
            Action sizeColumns = () =>
            {
                int bodyH = pnlBody.Height;
                int bodyW = pnlBody.Width;
                int rightW = 360;
                int listW = bodyW - rightW - GAP - PAD * 2;

                lvProducts.Location = new Point(PAD, PAD);
                lvProducts.Width = Math.Max(listW, 200);
                lvProducts.Height = bodyH - PAD * 2;

                pnlRight.Location = new Point(PAD + listW + GAP, PAD);
                pnlRight.Width = rightW;
                pnlRight.Height = bodyH - PAD * 2;
            };
            pnlBody.Resize += (s, e) => sizeColumns();
            tabMagazin.Resize += (s, e) => sizeColumns();

            // ── Right panel contents ─────────────────────────────────────
            //  Accent bar at top of right panel
            Panel rightTopBar = new Panel { BackColor = C_AccBlue, Dock = DockStyle.Top, Height = 3 };

            // Price area
            Panel pnlPriceArea = new Panel
            {
                BackColor = C_BgCard,
                Dock = DockStyle.Top,
                Height = 90,
                Padding = new Padding(PAD, 12, PAD, 0)
            };

            Label lblPriceCaption = new Label
            {
                Text = "UNIT PRICE",
                ForeColor = C_TextSec,
                Font = F_Tiny,
                Location = new Point(PAD, 14),
                AutoSize = true
            };
            lblPriceBig = new Label
            {
                Text = "—",
                ForeColor = C_AccGreen,
                Font = F_PriceBig,
                Location = new Point(PAD, 28),
                AutoSize = true
            };
            lblStockBadge = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 7.5f, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(PAD, 68),
                Padding = new Padding(6, 3, 6, 3)
            };
            pnlPriceArea.Controls.AddRange(new Control[] { lblPriceCaption, lblPriceBig, lblStockBadge });

            // Separator
            Panel sep1 = new Panel { BackColor = C_Border, Dock = DockStyle.Top, Height = 1 };

            // Description area
            Panel pnlDescArea = new Panel
            {
                BackColor = C_BgSurface,
                Dock = DockStyle.Top,
                Height = 110
            };
            Label lblDescCaption = new Label
            {
                Text = "DESCRIPTION",
                ForeColor = C_TextSec,
                Font = F_Tiny,
                Location = new Point(PAD, 12),
                AutoSize = true
            };
            lblDescText = new Label
            {
                Text = "Select a product to view details.",
                ForeColor = C_TextSec,
                Font = F_Small,
                Location = new Point(PAD, 28),
                Size = new Size(320, 72),
                AutoSize = false
            };
            pnlDescArea.Controls.AddRange(new Control[] { lblDescCaption, lblDescText });

            // Separator
            Panel sep2 = new Panel { BackColor = C_Border, Dock = DockStyle.Top, Height = 1 };

            // Quantity + cart actions
            Panel pnlCartArea = new Panel
            {
                BackColor = C_BgSurface,
                Dock = DockStyle.Top,
                Height = 160
            };

            Label lblQtyCaption = new Label
            {
                Text = "QUANTITY",
                ForeColor = C_TextSec,
                Font = F_Tiny,
                Location = new Point(PAD, 14),
                AutoSize = true
            };
            nudQuantity = new NumericUpDown
            {
                Location = new Point(PAD, 30),
                Width = 90,
                Minimum = 1,
                Maximum = 999,
                Value = 1,
                BackColor = C_BgInput,
                ForeColor = C_TextPrim,
                Font = F_Body
            };

            Button btnAddToCart = MakeBtn("🛒  Add to Cart", C_AccGreen, PAD, 74, 320, 36);
            Button btnViewCart = MakeBtn("📋  View Cart", C_AccBlue, PAD, 116, 154, 34);
            Button btnPlaceOrder = MakeBtn("✅  Place Order", C_AccAmber, PAD + 166, 116, 154, 34);

            btnAddToCart.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnViewCart.Font = F_Body;
            btnPlaceOrder.Font = F_Body;

            btnAddToCart.Click += (s, e) =>
            {
                if (lvProducts.SelectedItems.Count > 0 && lvProducts.SelectedItems[0].Tag is Produs sp)
                {
                    int qty = (int)nudQuantity.Value;
                    if (qty > sp.Cantitate) { ShowFeedback($"⚠  Only {sp.Cantitate} in stock.", false); return; }
                    var ex = shoppingCart.FirstOrDefault(c => c.Produs.Cod == sp.Cod);
                    if (ex != null) ex.Quantity += qty;
                    else shoppingCart.Add(new CartItem { Produs = sp, Quantity = qty });
                    UpdateCartBadge();
                    ShowFeedback($"✔  {qty}×  {sp.Nume} added to cart.", true);
                }
                else ShowFeedback("⚠  Select a product first.", false);
            };

            btnViewCart.Click += (s, e) =>
            {
                if (shoppingCart.Count == 0) { ShowFeedback("Cart is empty.", false); return; }
                using (var cf = new CartFormEditable(shoppingCart)) { cf.ShowDialog(); UpdateCartBadge(); }
            };

            btnPlaceOrder.Click += (s, e) =>
            {
                if (shoppingCart.Count == 0) { ShowFeedback("⚠  Cart is empty.", false); return; }
                foreach (var item in shoppingCart)
                {
                    DatabaseHelper.ScadeStocProdus(item.Produs.Cod, item.Quantity);
                    item.Produs.Cantitate -= item.Quantity;
                }
                shoppingCart.Clear();
                UpdateCartBadge();
                LoadProducts();
                LvProducts_SelectedIndexChanged(null, null);
                ShowFeedback("✔  Order placed successfully!", true);
            };

            pnlCartArea.Controls.AddRange(new Control[] { lblQtyCaption, nudQuantity, btnAddToCart, btnViewCart, btnPlaceOrder });

            pnlRight.Controls.AddRange(new Control[] {
                pnlCartArea, sep2, pnlDescArea, sep1, pnlPriceArea, rightTopBar });

            pnlBody.Controls.AddRange(new Control[] { lvProducts, pnlRight });

            tabMagazin.Controls.AddRange(new Control[] { pnlBody, pnlSearch });
            sizeColumns();
        }

        // ═══════════════════════════════════════════════════════════════════
        //  TAB 2 — MY ACCOUNT
        // ═══════════════════════════════════════════════════════════════════
        private void BuildTabAccount()
        {
            // Scroll container
            Panel pnlScroll = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = C_BgPanel,
                AutoScroll = true,
                Padding = new Padding(PAD)
            };

            // ── Two-column symmetric layout ──────────────────────────────
            // Column 1: Personal data    Column 2: Change password
            // Row 2 (full width): Account info card

            int colW = 400;
            int totalW = colW * 2 + GAP * 2;
            int startX = 0;   // will be centered on resize

            // Center the content block
            Panel pnlCenter = new Panel
            {
                BackColor = Color.Transparent,
                Width = totalW,
                Height = 700,
                Location = new Point((pnlScroll.ClientSize.Width - totalW) / 2, PAD)
            };

            pnlScroll.Resize += (s, e) =>
            {
                pnlCenter.Location = new Point(
                    Math.Max((pnlScroll.ClientSize.Width - totalW) / 2, PAD), PAD);
            };

            // ── Page title ───────────────────────────────────────────────
            Label lblPageTitle = new Label
            {
                Text = "Account Settings",
                ForeColor = C_TextPrim,
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                Location = new Point(0, 0),
                AutoSize = true
            };
            Label lblPageSub = new Label
            {
                Text = $"Logged in as: {currentUser?.Username ?? "Guest"}  ·  Role: {currentUser?.Rol ?? "—"}  ·  ID: {currentUser?.ID.ToString() ?? "—"}",
                ForeColor = C_TextSec,
                Font = F_Small,
                Location = new Point(0, 30),
                AutoSize = true
            };

            Panel titleDivider = new Panel
            {
                BackColor = C_Border,
                Location = new Point(0, 54),
                Size = new Size(totalW, 1)
            };

            // ── Card 1: Personal details (left) ──────────────────────────
            Panel card1 = MakeCard(0, 68, colW, 340, "Personal Details");

            const int CF = 16;  // card field x offset

            Label MkL(string t, int y) => new Label { Text = t, ForeColor = C_TextSec, Font = F_Small, Location = new Point(CF, y), AutoSize = true };
            TextBox MkT(int y) => new TextBox
            {
                Location = new Point(CF, y),
                Width = colW - CF * 2,
                BackColor = C_BgInput,
                ForeColor = C_TextPrim,
                BorderStyle = BorderStyle.FixedSingle,
                Font = F_Body
            };

            var l1 = MkL("FIRST NAME *", 48); txtNume = MkT(64);
            var l2 = MkL("LAST NAME *", 108); txtPrenume = MkT(124);
            var l3 = MkL("PHONE", 168); txtTelefon = MkT(184);
            var l4 = MkL("EMAIL *", 228); txtEmail = MkT(244);
            var l5 = MkL("ADDRESS", 288); txtAdresa = MkT(304);

            lblStatusDate = new Label
            {
                Text = "",
                ForeColor = C_AccGreen,
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                Location = new Point(CF, 340),
                Size = new Size(colW - CF * 2, 18),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Button btnSave = MakeCardBtn("💾  Save Changes", C_AccGreen, CF, 364, colW - CF * 2, 36);
            btnSave.Click += BtnSalveazaDate_Click;

            card1.Controls.AddRange(new Control[] {
                l1, txtNume, l2, txtPrenume, l3, txtTelefon, l4, txtEmail, l5, txtAdresa,
                lblStatusDate, btnSave });
            card1.Height = 416;

            // ── Card 2: Change password (right) ──────────────────────────
            Panel card2 = MakeCard(colW + GAP * 2, 68, colW, 340, "Change Password");

            Label MkL2(string t, int y) => new Label { Text = t, ForeColor = C_TextSec, Font = F_Small, Location = new Point(CF, y), AutoSize = true };
            TextBox MkPwd(int y) => new TextBox
            {
                Location = new Point(CF, y),
                Width = colW - CF * 2,
                BackColor = C_BgInput,
                ForeColor = C_TextPrim,
                BorderStyle = BorderStyle.FixedSingle,
                Font = F_Body,
                PasswordChar = '●'
            };

            var lp1 = MkL2("CURRENT PASSWORD", 48); txtParolaVeche = MkPwd(64);
            var lp2 = MkL2("NEW PASSWORD", 108); txtParolaNouA = MkPwd(124);

            // Password strength bar
            Label lblStrLbl = new Label { Text = "STRENGTH", ForeColor = C_TextSec, Font = F_Tiny, Location = new Point(CF, 158), AutoSize = true };
            ProgressBar pbStr = new ProgressBar
            {
                Location = new Point(CF, 172),
                Size = new Size(colW - CF * 2, 8),
                Minimum = 0,
                Maximum = 100,
                Style = ProgressBarStyle.Continuous
            };
            Label lblStrTxt = new Label { Text = "", ForeColor = C_TextSec, Font = F_Tiny, Location = new Point(CF, 183), AutoSize = true };

            txtParolaNouA.TextChanged += (s, e) =>
            {
                int p2 = CalcStrength(txtParolaNouA.Text);
                pbStr.Value = p2;
                lblStrTxt.Text = p2 < 30 ? "Weak" : p2 < 60 ? "Fair" : p2 < 85 ? "Good" : "Excellent";
                lblStrTxt.ForeColor = p2 < 30 ? C_AccRed : p2 < 60 ? C_AccAmber : p2 < 85 ? Color.YellowGreen : C_AccGreen;
            };

            var lp3 = MkL2("CONFIRM PASSWORD", 200); txtParolaConfirm = MkPwd(216);

            Label lblRules = new Label
            {
                Text = "✔ 8+ characters   ✔ Uppercase letter   ✔ Digit",
                ForeColor = Color.FromArgb(100, 140, 180),
                Font = F_Tiny,
                Location = new Point(CF, 254),
                AutoSize = true
            };

            CheckBox chkShow = new CheckBox
            {
                Text = "Show passwords",
                ForeColor = C_TextSec,
                Font = F_Small,
                Location = new Point(CF, 272),
                AutoSize = true
            };
            chkShow.CheckedChanged += (s, e) =>
            {
                char c = chkShow.Checked ? '\0' : '●';
                txtParolaVeche.PasswordChar = c;
                txtParolaNouA.PasswordChar = c;
                txtParolaConfirm.PasswordChar = c;
            };

            lblStatusParola = new Label
            {
                Text = "",
                ForeColor = C_AccGreen,
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                Location = new Point(CF, 296),
                Size = new Size(colW - CF * 2, 18),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Button btnChangePass = MakeCardBtn("🔑  Change Password", C_AccBlue, CF, 320, colW - CF * 2, 36);
            btnChangePass.Click += BtnSchimbaParola_Click;

            card2.Controls.AddRange(new Control[] {
                lp1, txtParolaVeche, lp2, txtParolaNouA,
                lblStrLbl, pbStr, lblStrTxt,
                lp3, txtParolaConfirm,
                lblRules, chkShow, lblStatusParola, btnChangePass });
            card2.Height = 372;

            // ── Card 3: Account info (full width, bottom) ────────────────
            int infoTop = Math.Max(card1.Bottom, card2.Bottom) + GAP * 2;
            Panel card3 = MakeCard(0, infoTop, totalW, 80, "Account Information");

            int infoColW = (totalW - CF * 4) / 3;

            void AddInfoPair(string lbl, string val, int x)
            {
                card3.Controls.Add(new Label { Text = lbl, ForeColor = C_TextSec, Font = F_Tiny, Location = new Point(x, 48), AutoSize = true });
                card3.Controls.Add(new Label { Text = val, ForeColor = C_TextPrim, Font = F_SubHead, Location = new Point(x, 60), AutoSize = true });
            }

            AddInfoPair("USERNAME", currentUser?.Username ?? "—", CF);
            AddInfoPair("ROLE", currentUser?.Rol ?? "—", CF + infoColW + GAP);
            AddInfoPair("ACCOUNT ID", currentUser?.ID.ToString() ?? "—", CF + (infoColW + GAP) * 2);

            Button btnRefresh = MakeCardBtn("🔄  Reload Data", Color.FromArgb(40, 56, 72), totalW - 180 - CF, 48, 180, 30);
            btnRefresh.Click += (s, e) => { IncarcaDateCont(); SetStatus(lblStatusDate, "Data reloaded.", true); };
            card3.Controls.Add(btnRefresh);
            card3.Height = 96;

            pnlCenter.Height = card3.Bottom + PAD;
            pnlCenter.Controls.AddRange(new Control[] {
                lblPageTitle, lblPageSub, titleDivider,
                card1, card2, card3 });

            pnlScroll.Controls.Add(pnlCenter);
            tabCont.Controls.Add(pnlScroll);
        }

        // ── Card factory ─────────────────────────────────────────────────
        private Panel MakeCard(int x, int y, int w, int h, string title)
        {
            var pnl = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(w, h),
                BackColor = C_BgSurface
            };

            // Top accent
            Panel topBar = new Panel { BackColor = C_AccBlue, Dock = DockStyle.Top, Height = 3 };

            Label lblTitle = new Label
            {
                Text = title,
                ForeColor = C_TextPrim,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Location = new Point(16, 12),
                AutoSize = true
            };

            Panel divider = new Panel
            {
                BackColor = C_Border,
                Location = new Point(0, 34),
                Size = new Size(w, 1)
            };

            pnl.Controls.AddRange(new Control[] { topBar, lblTitle, divider });
            return pnl;
        }

        private Button MakeCardBtn(string text, Color bg, int x, int y, int w, int h)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, h),
                BackColor = bg,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = F_Body,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        // ─────────────────────────────────────────────────────────────────
        //  EVENT HANDLERS
        // ─────────────────────────────────────────────────────────────────
        private void LvProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvProducts?.SelectedItems.Count > 0 && lvProducts.SelectedItems[0].Tag is Produs p)
            {
                lblPriceBig.Text = $"{p.Pret:N2} MDL";

                if (p.Cantitate == 0)
                {
                    lblStockBadge.Text = "  OUT OF STOCK  ";
                    lblStockBadge.BackColor = Color.FromArgb(100, 30, 34);
                    lblStockBadge.ForeColor = Color.FromArgb(255, 120, 128);
                    nudQuantity.Maximum = 0;
                }
                else if (p.Cantitate < 5)
                {
                    lblStockBadge.Text = $"  LOW STOCK — {p.Cantitate} left  ";
                    lblStockBadge.BackColor = Color.FromArgb(80, 56, 16);
                    lblStockBadge.ForeColor = C_AccAmber;
                    nudQuantity.Maximum = p.Cantitate;
                    nudQuantity.Value = 1;
                }
                else
                {
                    lblStockBadge.Text = $"  IN STOCK — {p.Cantitate} available  ";
                    lblStockBadge.BackColor = Color.FromArgb(20, 60, 40);
                    lblStockBadge.ForeColor = C_AccGreen;
                    nudQuantity.Maximum = p.Cantitate;
                    nudQuantity.Value = 1;
                }

                lblDescText.Text = string.IsNullOrWhiteSpace(p.Descriere)
                    ? "(No description available)"
                    : p.Descriere;
            }
            else
            {
                if (lblPriceBig != null) lblPriceBig.Text = "—";
                if (lblStockBadge != null) { lblStockBadge.Text = ""; lblStockBadge.BackColor = Color.Transparent; }
                if (lblDescText != null) lblDescText.Text = "Select a product to view details.";
            }
        }

        private void BtnSalveazaDate_Click(object sender, EventArgs e)
        {
            if (currentUser == null) { SetStatus(lblStatusDate, "Not logged in.", false); return; }
            if (string.IsNullOrWhiteSpace(txtNume.Text) || string.IsNullOrWhiteSpace(txtPrenume.Text))
            { SetStatus(lblStatusDate, "First and last name are required.", false); return; }
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            { SetStatus(lblStatusDate, "Invalid email address.", false); return; }
            if (!string.IsNullOrWhiteSpace(txtTelefon.Text) && txtTelefon.Text.Length < 8)
            { SetStatus(lblStatusDate, "Phone number is too short.", false); return; }

            bool ok = DatabaseHelper.UpdateDateClient(
                currentUser.ID,
                txtNume.Text.Trim(), txtPrenume.Text.Trim(),
                txtTelefon.Text.Trim(), txtEmail.Text.Trim(), txtAdresa.Text.Trim());

            if (ok) { SetStatus(lblStatusDate, "✔  Saved successfully!", true); lblWelcome.Text = $"Welcome,  {txtNume.Text.Trim()} {txtPrenume.Text.Trim()}"; }
            else SetStatus(lblStatusDate, "✘  Save failed. Try again.", false);
        }

        private void BtnSchimbaParola_Click(object sender, EventArgs e)
        {
            if (currentUser == null) { SetStatus(lblStatusParola, "Not logged in.", false); return; }
            string veche = txtParolaVeche.Text, noua = txtParolaNouA.Text, confirma = txtParolaConfirm.Text;
            if (string.IsNullOrEmpty(veche) || string.IsNullOrEmpty(noua) || string.IsNullOrEmpty(confirma))
            { SetStatus(lblStatusParola, "Fill in all fields.", false); return; }
            if (DatabaseHelper.Login(currentUser.Username, veche) == null)
            { SetStatus(lblStatusParola, "✘  Current password is incorrect.", false); txtParolaVeche.Clear(); return; }
            if (noua.Length < 8)
            { SetStatus(lblStatusParola, "✘  Password must be at least 8 characters.", false); return; }
            if (!System.Text.RegularExpressions.Regex.IsMatch(noua, "[A-Z]"))
            { SetStatus(lblStatusParola, "✘  Password must contain an uppercase letter.", false); return; }
            if (!System.Text.RegularExpressions.Regex.IsMatch(noua, "[0-9]"))
            { SetStatus(lblStatusParola, "✘  Password must contain a digit.", false); return; }
            if (noua != confirma)
            { SetStatus(lblStatusParola, "✘  Passwords do not match.", false); txtParolaConfirm.Clear(); return; }
            if (veche == noua)
            { SetStatus(lblStatusParola, "✘  New password must differ from current.", false); return; }

            if (DatabaseHelper.SchimbaParola(currentUser.ID, noua))
            {
                SetStatus(lblStatusParola, "✔  Password changed successfully!", true);
                txtParolaVeche.Clear(); txtParolaNouA.Clear(); txtParolaConfirm.Clear();
                currentUser.Password = noua;
            }
            else SetStatus(lblStatusParola, "✘  Error changing password.", false);
        }

        // ─────────────────────────────────────────────────────────────────
        //  HELPERS
        // ─────────────────────────────────────────────────────────────────
        private void LoadProducts() => DisplayProducts(DatabaseHelper.GetAllProduse());

        private void DisplayProducts(IEnumerable<Produs> list)
        {
            lvProducts.Items.Clear();
            int i = 0;
            foreach (var p in list)
            {
                var lvi = new ListViewItem(p.Nume);
                lvi.SubItems.Add(p.Categorie);
                lvi.SubItems.Add($"{p.Pret:N2} MDL");
                lvi.Tag = p;
                lvi.BackColor = i % 2 == 0 ? C_BgSurface : Color.FromArgb(30, 40, 54);

                if (p.Cantitate == 0) lvi.ForeColor = Color.FromArgb(200, 90, 96);
                else if (p.Cantitate < 5) lvi.ForeColor = C_AccAmber;
                else lvi.ForeColor = C_TextPrim;

                lvProducts.Items.Add(lvi);
                i++;
            }
        }

        private void DoSearch()
        {
            string term = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(term) || term == "Search by name or category…") { LoadProducts(); return; }
            var results = DatabaseHelper.SearchProduse(term);
            DisplayProducts(results);
            ShowFeedback(results.Count == 0 ? "No products found." : $"{results.Count} result(s) found.", results.Count > 0);
        }

        private void UpdateCartBadge()
        {
            int total = shoppingCart.Sum(c => c.Quantity);
            lblCartCount.Text = $"🛒  {total} item{(total != 1 ? "s" : "")}";
        }

        private void ShowFeedback(string msg, bool ok)
        {
            lblFeedback.Text = msg;
            lblFeedback.ForeColor = ok ? C_AccGreen : C_AccAmber;
        }

        private void SetStatus(Label lbl, string msg, bool ok)
        {
            lbl.Text = msg;
            lbl.ForeColor = ok ? C_AccGreen : C_AccRed;
        }

        private void IncarcaDateCont()
        {
            if (currentUser == null) return;
            try
            {
                var c = DatabaseHelper.GetClientById(currentUser.ID);
                if (c != null)
                {
                    txtNume.Text = c.Nume ?? "";
                    txtPrenume.Text = c.Prenume ?? "";
                    txtTelefon.Text = c.Telefon ?? "";
                    txtEmail.Text = c.Email ?? "";
                    txtAdresa.Text = c.Adresa ?? "";
                }
            }
            catch { SetStatus(lblStatusDate, "Could not load account data.", false); }
        }

        private int CalcStrength(string p)
        {
            if (string.IsNullOrEmpty(p)) return 0;
            int s = 0;
            if (p.Length >= 8) s += 20;
            if (p.Length >= 12) s += 15;
            if (System.Text.RegularExpressions.Regex.IsMatch(p, "[A-Z]")) s += 20;
            if (System.Text.RegularExpressions.Regex.IsMatch(p, "[a-z]")) s += 10;
            if (System.Text.RegularExpressions.Regex.IsMatch(p, "[0-9]")) s += 20;
            if (System.Text.RegularExpressions.Regex.IsMatch(p, "[^a-zA-Z0-9]")) s += 15;
            return Math.Min(s, 100);
        }

        private Button MakeBtn(string text, Color bg, int x, int y, int w, int h)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, h),
                BackColor = bg,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = F_Body,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private Button MakeHeaderBtn(string text, Color bg)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(84, 30),
                BackColor = bg,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = F_Small,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
    }

    // ═══════════════════════════════════════════════════════════════════════
    //  EDITABLE CART  (style updated to match)
    // ═══════════════════════════════════════════════════════════════════════
    public class CartFormEditable : Form
    {
        private List<CartItem> _cart;
        private ListView lvCart;
        private Label lblTotal;

        private static readonly Color CB = Color.FromArgb(26, 34, 46);
        private static readonly Color CC = Color.FromArgb(22, 30, 42);
        private static readonly Color CP = Color.FromArgb(230, 236, 244);
        private static readonly Color CG = Color.FromArgb(48, 168, 108);
        private static readonly Color CR = Color.FromArgb(196, 64, 72);
        private static readonly Color CBl = Color.FromArgb(56, 132, 220);
        private static readonly Color CGr = Color.FromArgb(42, 54, 70);
        private static readonly Font FB = new Font("Segoe UI", 9f);

        public CartFormEditable(List<CartItem> cartItems)
        {
            _cart = cartItems;
            this.Text = "Your Shopping Cart";
            this.Size = new Size(580, 480);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = CB;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Top accent
            Panel topBar = new Panel { BackColor = CBl, Dock = DockStyle.Top, Height = 3 };

            // Header
            Panel pnlHeader = new Panel { BackColor = Color.FromArgb(18, 24, 32), Dock = DockStyle.Top, Height = 48 };
            Label lblT = new Label { Text = "Shopping Cart", ForeColor = CP, Font = new Font("Segoe UI", 11f, FontStyle.Bold), Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(16, 0, 0, 0) };
            pnlHeader.Controls.AddRange(new Control[] { lblT });

            lvCart = new ListView
            {
                Location = new Point(16, 70),
                Size = new Size(540, 260),
                View = View.Details,
                FullRowSelect = true,
                GridLines = false,
                BackColor = CGr,
                ForeColor = CP,
                Font = FB,
                BorderStyle = BorderStyle.None
            };
            lvCart.Columns.Add("Product", 210);
            lvCart.Columns.Add("Unit Price", 95);
            lvCart.Columns.Add("Qty", 55);
            lvCart.Columns.Add("Subtotal", 155);

            lblTotal = new Label
            {
                ForeColor = CG,
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                Location = new Point(16, 344),
                AutoSize = true
            };

            Label lblChg = new Label { Text = "Change qty:", ForeColor = CP, Font = FB, Location = new Point(16, 388), AutoSize = true };
            NumericUpDown nud = new NumericUpDown
            {
                Location = new Point(108, 385),
                Width = 70,
                Minimum = 1,
                Maximum = 999,
                Value = 1,
                BackColor = CC,
                ForeColor = CP,
                Font = FB
            };

            Button btnUpd = Btn("Update", CBl, 190, 385, 90);
            Button btnRem = Btn("Remove", CR, 290, 385, 90);
            Button btnCls = Btn("Close", Color.FromArgb(52, 68, 88), 420, 385, 100);

            btnUpd.Click += (s, e) => { if (lvCart.SelectedItems.Count == 0) return; _cart[lvCart.SelectedIndices[0]].Quantity = (int)nud.Value; Refresh2(); };
            btnRem.Click += (s, e) => { if (lvCart.SelectedItems.Count == 0) return; _cart.RemoveAt(lvCart.SelectedIndices[0]); Refresh2(); };
            btnCls.Click += (s, e) => this.Close();

            Refresh2();
            this.Controls.AddRange(new Control[] { topBar, pnlHeader, lvCart, lblTotal, lblChg, nud, btnUpd, btnRem, btnCls });
        }

        private void Refresh2()
        {
            lvCart.Items.Clear();
            decimal grand = 0;
            int i = 0;
            foreach (var item in _cart)
            {
                var lvi = new ListViewItem(item.Produs.Nume);
                lvi.SubItems.Add($"{item.Produs.Pret:N2} MDL");
                lvi.SubItems.Add(item.Quantity.ToString());
                lvi.SubItems.Add($"{item.TotalPrice:N2} MDL");
                lvi.BackColor = i++ % 2 == 0 ? Color.FromArgb(42, 54, 70) : Color.FromArgb(36, 46, 60);
                lvi.ForeColor = CP;
                lvCart.Items.Add(lvi);
                grand += item.TotalPrice;
            }
            lblTotal.Text = $"Total:   {grand:N2} MDL";
        }

        private Button Btn(string text, Color bg, int x, int y, int w)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, 30),
                BackColor = bg,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5f),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
    }
}