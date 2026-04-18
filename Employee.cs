using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MoldCom
{
    public class Employee : Form
    {
        private TextBox txtSearch, txtProductCode, txtNewQuantity;
        private ListView lvProducts;
        private Button btnSearch, btnUpdateStock, btnLogout;
        private Label lblLowStock, lblStatusUpdate;

        // ── Color palette (unchanged) ─────────────────────────────────────
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

        // ── Fonts (unchanged) ─────────────────────────────────────────────
        private static readonly Font F_Logo = new Font("Segoe UI", 11f, FontStyle.Bold);
        private static readonly Font F_SubHead = new Font("Segoe UI", 10f, FontStyle.Bold);
        private static readonly Font F_Body = new Font("Segoe UI", 9f);
        private static readonly Font F_Small = new Font("Segoe UI", 8f);
        private static readonly Font F_Tiny = new Font("Segoe UI", 7.5f);

        // ── Layout constants ──────────────────────────────────────────────
        // All spacing decisions live here — change one value to adjust globally.
        private const int OUTER_PAD = 24;   // body outer margin on all sides
        private const int INNER_GAP = 16;   // gap between list column and right panel
        private const int SECTION_SP = 12;   // vertical space between search bar and list
        private const int HEADER_H = 56;
        private const int STATS_H = 76;
        private const int SEARCH_H = 48;
        private const int RIGHT_W = 272;  // right panel fixed width

        private readonly DateTime _loginTime = DateTime.Now;

        public Employee()
        {
            InitializeComponent();
            LoadProducts();
        }

        // ─────────────────────────────────────────────────────────────────
        private void LoadProducts(IEnumerable<Produs> lista = null)
        {
            var produse = lista ?? DatabaseHelper.GetAllProduse();
            lvProducts.Items.Clear();
            int i = 0;
            foreach (var p in produse)
            {
                var item = new ListViewItem(p.Cod);
                item.SubItems.Add(p.Nume);
                item.SubItems.Add(p.Cantitate.ToString());
                item.SubItems.Add(p.Locatie);
                item.Tag = p;
                item.BackColor = i % 2 == 0 ? C_BgCard : Color.FromArgb(38, 50, 64);

                if (p.Cantitate == 0) item.ForeColor = Color.FromArgb(200, 90, 96);
                else if (p.Cantitate < 5) item.ForeColor = C_AccAmber;
                else item.ForeColor = C_TextPrim;

                lvProducts.Items.Add(item);
                i++;
            }
        }

        // ─────────────────────────────────────────────────────────────────
        private void InitializeComponent()
        {
            this.Text = "MoldCom — Employee Dashboard";
            this.Size = new Size(1100, 750);
            this.MinimumSize = new Size(960, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = C_BgPanel;

            // ═════════════════════════════════════════════════════════════
            // HEADER
            // Fixed-height top bar: accent line | logo | centred clock | logout
            // ═════════════════════════════════════════════════════════════
            Panel pnlHeader = new Panel
            {
                BackColor = C_BgBase,
                Dock = DockStyle.Top,
                Height = HEADER_H
            };
            Panel topLine = new Panel { BackColor = C_AccBlue, Dock = DockStyle.Top, Height = 3 };
            Panel headerDiv = new Panel { BackColor = C_Border, Dock = DockStyle.Bottom, Height = 1 };

            Label lblTitle = new Label
            {
                Text = "MOLDCOM",
                ForeColor = C_TextPrim,
                Font = F_Logo,
                Location = new Point(OUTER_PAD, 13),
                AutoSize = true
            };
            Label lblTitleSub = new Label
            {
                Text = "WAREHOUSE  &  STOCK CONTROL",
                ForeColor = C_AccBlue,
                Font = new Font("Segoe UI", 6.5f, FontStyle.Bold),
                Location = new Point(OUTER_PAD, 34),
                AutoSize = true
            };

            // Clock centred horizontally; vertically centred inside the usable header height
            Label lblClock = new Label
            {
                Text = "",
                ForeColor = C_TextSec,
                Font = F_Tiny,
                AutoSize = true
            };

            btnLogout = new Button
            {
                Text = "Logout",
                Size = new Size(84, 30),
                BackColor = Color.FromArgb(70, 30, 34),
                ForeColor = Color.FromArgb(220, 110, 118),
                FlatStyle = FlatStyle.Flat,
                Font = F_Small,
                Cursor = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) => this.Close();

            pnlHeader.Resize += (s, e) =>
            {
                // Centre vertically in the area below the 3-px accent line
                int midY = (HEADER_H + 3) / 2;
                lblClock.Location = new Point(pnlHeader.Width / 2 - lblClock.Width / 2,
                                               midY - lblClock.Height / 2);
                btnLogout.Location = new Point(pnlHeader.Width - btnLogout.Width - OUTER_PAD,
                                               midY - btnLogout.Height / 2);
            };

            var timer = new System.Windows.Forms.Timer { Interval = 1000, Enabled = true };
            timer.Tick += (s, e) =>
            {
                TimeSpan elapsed = DateTime.Now - _loginTime;
                lblClock.Text = $"Session: {elapsed:hh\\:mm\\:ss}  ·  {DateTime.Now:HH:mm, dddd}";
                lblClock.Left = pnlHeader.Width / 2 - lblClock.Width / 2;
            };
            this.FormClosed += (s, e) => timer.Dispose();

            pnlHeader.Controls.AddRange(new Control[]
                { topLine, headerDiv, lblTitle, lblTitleSub, lblClock, btnLogout });

            // ═════════════════════════════════════════════════════════════
            // STATS BAR
            // Four equal-width cards in a flow row below the header
            // ═════════════════════════════════════════════════════════════
            Panel pnlStats = new Panel
            {
                BackColor = C_BgSurface,
                Dock = DockStyle.Top,
                Height = STATS_H
            };
            Panel statsDiv = new Panel { BackColor = C_Border, Dock = DockStyle.Bottom, Height = 1 };

            // Horizontal padding matches OUTER_PAD so cards align with body content below
            FlowLayoutPanel flowStats = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent,
                Padding = new Padding(OUTER_PAD, 8, OUTER_PAD, 8)
            };
            pnlStats.Controls.AddRange(new Control[] { statsDiv, flowStats });

            Label lblStatTotal = MakeStatMini("—", "Total Products", C_AccBlue);
            Label lblStatLow = MakeStatMini("—", "Low Stock (< 5)", C_AccAmber);
            Label lblStatOut = MakeStatMini("—", "Out of Stock", C_AccRed);
            Label lblStatToday = MakeStatMini(DateTime.Now.ToString("dd MMM yyyy"), "Today's Date", C_AccGreen);

            foreach (var (lbl, accent) in new[] {
                (lblStatTotal, C_AccBlue),
                (lblStatLow,   C_AccAmber),
                (lblStatOut,   C_AccRed),
                (lblStatToday, C_AccGreen) })
            {
                flowStats.Controls.Add(WrapStat(lbl, accent, 190));
            }

            // ═════════════════════════════════════════════════════════════
            // BODY  (fills remaining space)
            // ═════════════════════════════════════════════════════════════
            Panel pnlBody = new Panel
            {
                BackColor = C_BgPanel,
                Dock = DockStyle.Fill,
                Padding = new Padding(OUTER_PAD)
            };

            // ── Search bar ────────────────────────────────────────────────
            Panel pnlSearch = new Panel { BackColor = C_BgSurface };
            Panel searchDiv = new Panel { BackColor = C_Border, Dock = DockStyle.Bottom, Height = 1 };

            // Controls vertically centred inside the search panel height
            int scy = (SEARCH_H - 22) / 2;   // 22 ≈ textbox/button rendered height

            txtSearch = new TextBox
            {
                Location = new Point(12, scy),
                Width = 230,
                BackColor = C_BgInput,
                ForeColor = C_TextSec,
                BorderStyle = BorderStyle.FixedSingle,
                Font = F_Body
            };
            txtSearch.Text = "Search by name or code…";
            txtSearch.GotFocus += (s, e) =>
            {
                if (txtSearch.ForeColor == C_TextSec)
                { txtSearch.Text = ""; txtSearch.ForeColor = C_TextPrim; }
            };
            txtSearch.LostFocus += (s, e) =>
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                { txtSearch.Text = "Search by name or code…"; txtSearch.ForeColor = C_TextSec; }
            };
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnSearch_Click(s, e); };

            int bx = txtSearch.Right + 10;
            btnSearch = MakeBtn("Search", C_AccBlue, bx, scy - 2, 80, 26);
            Button btnReset = MakeBtn("Reset", C_BgInput, bx + 88, scy - 2, 66, 26);

            Label lblCount = new Label
            {
                Text = "",
                ForeColor = C_TextSec,
                Font = F_Tiny,
                Location = new Point(bx + 164, scy + 3),
                AutoSize = true
            };

            btnSearch.Click += BtnSearch_Click;
            btnReset.Click += (s, e) =>
            {
                txtSearch.Text = "Search by name or code…";
                txtSearch.ForeColor = C_TextSec;
                LoadProducts();
                RefreshStats(lblStatTotal, lblStatLow, lblStatOut);
                lblCount.Text = $"{lvProducts.Items.Count} products";
            };

            pnlSearch.Controls.AddRange(new Control[]
                { searchDiv, txtSearch, btnSearch, btnReset, lblCount });

            // ── Product list ──────────────────────────────────────────────
            lvProducts = new ListView
            {
                BackColor = C_BgCard,
                ForeColor = C_TextPrim,
                Font = F_Body,
                View = View.Details,
                FullRowSelect = true,
                GridLines = false,
                BorderStyle = BorderStyle.None
            };
            lvProducts.Columns.AddRange(new ColumnHeader[]
            {
                new ColumnHeader { Text = "Code",     Width = 90  },
                new ColumnHeader { Text = "Name",     Width = 195 },
                new ColumnHeader { Text = "Quantity", Width = 80  },
                new ColumnHeader { Text = "Location", Width = 105 }
            });

            lvProducts.SelectedIndexChanged += (s, e) =>
            {
                if (lvProducts.SelectedItems.Count > 0 &&
                    lvProducts.SelectedItems[0].Tag is Produs p)
                {
                    txtProductCode.Text = p.Cod;
                    txtNewQuantity.Text = p.Cantitate.ToString();
                    lblLowStock.Visible = p.Cantitate < 5;
                    lblLowStock.Text = p.Cantitate == 0
                        ? "⚠  OUT OF STOCK"
                        : "⚠  LOW STOCK — quantity below 5";
                    lblLowStock.ForeColor = p.Cantitate == 0 ? C_AccRed : C_AccAmber;
                    lblStatusUpdate.Text = "";
                }
            };

            // ═════════════════════════════════════════════════════════════
            // RIGHT PANEL
            // Sections stacked via DockStyle:
            //   Top  → header bar  (44 px)
            //   Top  → form        (auto-height via TableLayoutPanel)
            //   Bottom → work session card  (110 px)
            //   Bottom → 1-px divider above the card
            // ═════════════════════════════════════════════════════════════
            Panel pnlRight = new Panel { BackColor = C_BgSurface };

            // — Right header —
            Panel rightHeader = new Panel { BackColor = C_BgBase, Dock = DockStyle.Top, Height = 44 };
            Panel rightAccent = new Panel { BackColor = C_AccBlue, Dock = DockStyle.Top, Height = 3 };
            Panel rightDiv = new Panel { BackColor = C_Border, Dock = DockStyle.Bottom, Height = 1 };
            Label lblRightTitle = new Label
            {
                Text = "Update Stock",
                ForeColor = C_TextPrim,
                Font = F_SubHead,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(14, 0, 0, 0)
            };
            rightHeader.Controls.AddRange(new Control[] { rightAccent, rightDiv, lblRightTitle });

            // — Work session card (anchored to bottom of right panel) —
            Panel pnlInfo = new Panel
            {
                BackColor = C_BgCard,
                Dock = DockStyle.Bottom,
                Height = 110,
                Padding = new Padding(14, 8, 14, 8)
            };
            Panel infoAccent = new Panel { BackColor = C_AccGreen, Dock = DockStyle.Top, Height = 3 };

            // Four evenly distributed rows inside the info card
            TableLayoutPanel tblInfo = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1,
                BackColor = Color.Transparent
            };
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            for (int r = 0; r < 4; r++)
                tblInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));

            Label MkInfoRow(string text) => new Label
            {
                Text = text,
                ForeColor = C_TextPrim,
                Font = F_Small,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label lblInfoTitle = new Label
            {
                Text = "WORK SESSION",
                ForeColor = C_TextSec,
                Font = F_Tiny,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            Label lblShiftStart = MkInfoRow($"Started:   {_loginTime:HH:mm:ss  ·  dd MMM yyyy}");
            Label lblRole = MkInfoRow("Role:   Warehouse Employee");
            Label lblUpdatesCount = MkInfoRow("Stock updates this session:   0");

            tblInfo.Controls.Add(lblInfoTitle, 0, 0);
            tblInfo.Controls.Add(lblShiftStart, 0, 1);
            tblInfo.Controls.Add(lblRole, 0, 2);
            tblInfo.Controls.Add(lblUpdatesCount, 0, 3);

            pnlInfo.Controls.Add(tblInfo);
            pnlInfo.Controls.Add(infoAccent);   // last → renders on top (DockStyle.Top)

            // 1-px separator between form and info card
            Panel midDivider = new Panel
            {
                BackColor = C_Border,
                Dock = DockStyle.Bottom,
                Height = 1
            };

            // — Update Stock form —
            // TableLayoutPanel gives each field pair consistent spacing automatically.
            TableLayoutPanel tblForm = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 1,
                RowCount = 8,
                BackColor = Color.Transparent,
                Padding = new Padding(14, 16, 14, 16),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };
            tblForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            Label MkFieldLabel(string text) => new Label
            {
                Text = text,
                ForeColor = C_TextSec,
                Font = F_Tiny,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 3),
                Height = 16
            };
            TextBox MkField() => new TextBox
            {
                BackColor = C_BgInput,
                ForeColor = C_TextPrim,
                BorderStyle = BorderStyle.FixedSingle,
                Font = F_Body,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 12)   // 12 px breathing room below each field
            };

            Label lblCode = MkFieldLabel("PRODUCT CODE");
            txtProductCode = MkField();
            Label lblQty = MkFieldLabel("NEW QUANTITY");
            txtNewQuantity = MkField();

            Label lblHint = new Label
            {
                Text = "Select a row from the list to auto-fill,\nor enter code and quantity manually.",
                ForeColor = C_TextSec,
                Font = F_Tiny,
                Dock = DockStyle.Fill,
                AutoSize = false,
                Height = 32,
                Margin = new Padding(0, 0, 0, 14)
            };

            lblLowStock = new Label
            {
                Text = "",
                ForeColor = C_AccAmber,
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                Dock = DockStyle.Fill,
                Visible = false,
                Height = 18,
                Margin = new Padding(0, 0, 0, 2)
            };

            lblStatusUpdate = new Label
            {
                Text = "",
                ForeColor = C_AccGreen,
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                Dock = DockStyle.Fill,
                Height = 18,
                Margin = new Padding(0, 0, 0, 10)
            };

            btnUpdateStock = new Button
            {
                Text = "Update Stock",
                BackColor = C_AccBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Dock = DockStyle.Fill,
                Height = 36,
                Cursor = Cursors.Hand,
                Margin = new Padding(0)
            };
            btnUpdateStock.FlatAppearance.BorderSize = 0;
            btnUpdateStock.Click += BtnUpdateStock_Click;

            tblForm.Controls.Add(lblCode, 0, 0);
            tblForm.Controls.Add(txtProductCode, 0, 1);
            tblForm.Controls.Add(lblQty, 0, 2);
            tblForm.Controls.Add(txtNewQuantity, 0, 3);
            tblForm.Controls.Add(lblHint, 0, 4);
            tblForm.Controls.Add(lblLowStock, 0, 5);
            tblForm.Controls.Add(lblStatusUpdate, 0, 6);
            tblForm.Controls.Add(btnUpdateStock, 0, 7);

            // Track stock-update count in the session info card
            int updatesThisSession = 0;
            btnUpdateStock.Click += (s, e) =>
            {
                if (lblStatusUpdate.ForeColor == C_AccGreen &&
                    lblStatusUpdate.Text.StartsWith("✔"))
                {
                    updatesThisSession++;
                    lblUpdatesCount.Text = $"Stock updates this session:   {updatesThisSession}";
                }
            };

            // Assemble right panel — Bottom items first, then Top
            pnlRight.Controls.Add(pnlInfo);      // Bottom
            pnlRight.Controls.Add(midDivider);   // Bottom (above pnlInfo)
            pnlRight.Controls.Add(tblForm);      // Top    (below rightHeader)
            pnlRight.Controls.Add(rightHeader);  // Top    (very first)

            // ═════════════════════════════════════════════════════════════
            // DYNAMIC LAYOUT
            // One Action recalculates all manual-position controls on resize.
            // ═════════════════════════════════════════════════════════════
            Action sizeBody = () =>
            {
                int bw = pnlBody.Width - OUTER_PAD * 2;
                int bh = pnlBody.Height - OUTER_PAD * 2;
                int listW = bw - RIGHT_W - INNER_GAP;

                // Extra top offset so search bar and list sit lower,
                // giving visible breathing room beneath the stats bar.
                const int TOP_OFFSET = 55;

                // Search bar — pushed down by TOP_OFFSET
                pnlSearch.Location = new Point(OUTER_PAD, OUTER_PAD + TOP_OFFSET);
                pnlSearch.Size = new Size(listW, SEARCH_H);

                // Product list — SECTION_SP gap below search bar
                lvProducts.Location = new Point(OUTER_PAD, OUTER_PAD + TOP_OFFSET + SEARCH_H + SECTION_SP);
                lvProducts.Size = new Size(listW, bh - TOP_OFFSET - SEARCH_H - SECTION_SP);

                // Right panel — top-aligned with search bar, same bottom as list
                pnlRight.Location = new Point(OUTER_PAD + listW + INNER_GAP, OUTER_PAD + TOP_OFFSET);
                pnlRight.Size = new Size(RIGHT_W, bh - TOP_OFFSET);
            };

            pnlBody.Resize += (s, e) => sizeBody();
            pnlBody.Controls.AddRange(new Control[] { pnlSearch, lvProducts, pnlRight });

            this.Controls.AddRange(new Control[] { pnlHeader, pnlStats, pnlBody });

            this.Load += (s, e) =>
            {
                sizeBody();
                RefreshStats(lblStatTotal, lblStatLow, lblStatOut);
                lblCount.Text = $"{lvProducts.Items.Count} products";
            };
        }

        // ── Stat card helpers ─────────────────────────────────────────────
        private Label MakeStatMini(string value, string caption, Color accent)
        {
            return new Label
            {
                Text = value,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 15f, FontStyle.Bold),
                AutoSize = true,
                Tag = caption
            };
        }

        private Panel WrapStat(Label lblVal, Color accent, int width)
        {
            var pnl = new Panel
            {
                Width = width,
                Height = STATS_H - 16,       // 8 px breathing room top & bottom
                BackColor = C_BgCard,
                Margin = new Padding(0, 0, 12, 0)   // 12 px gap between cards
            };
            Panel bar = new Panel { BackColor = accent, Dock = DockStyle.Left, Width = 4 };

            lblVal.Location = new Point(14, 7);

            Label lblCap = new Label
            {
                Text = lblVal.Tag?.ToString() ?? "",
                ForeColor = C_TextSec,
                Font = F_Tiny,
                Location = new Point(14, 40),
                AutoSize = true
            };

            pnl.Controls.AddRange(new Control[] { bar, lblVal, lblCap });
            return pnl;
        }

        private void RefreshStats(Label lblTotal, Label lblLow, Label lblOut)
        {
            var all = DatabaseHelper.GetAllProduse();
            lblTotal.Text = all.Count.ToString();
            lblLow.Text = all.Count(p => p.Cantitate > 0 && p.Cantitate < 5).ToString();
            lblOut.Text = all.Count(p => p.Cantitate == 0).ToString();
        }

        // ── Generic button factory ────────────────────────────────────────
        private Button MakeBtn(string text, Color bg, int x, int y, int w, int h)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, h),
                BackColor = bg,
                ForeColor = C_TextPrim,
                FlatStyle = FlatStyle.Flat,
                Font = F_Small,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        // ── Event handlers (logic unchanged) ─────────────────────────────
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string term = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(term) || term == "Search by name or code…")
            { LoadProducts(); return; }

            var rezultate = DatabaseHelper.SearchProduseEmployee(term);
            LoadProducts(rezultate);

            if (rezultate.Count == 0)
                lblStatusUpdate.Text = "No products found.";
        }

        private void BtnUpdateStock_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductCode.Text) ||
                string.IsNullOrWhiteSpace(txtNewQuantity.Text))
            {
                lblStatusUpdate.ForeColor = C_AccRed;
                lblStatusUpdate.Text = "⚠  Fill in product code and quantity.";
                return;
            }

            if (int.TryParse(txtNewQuantity.Text, out int qty) && qty >= 0)
            {
                string cod = txtProductCode.Text.Trim();
                bool ok = DatabaseHelper.UpdateStocProdus(cod, qty);
                if (ok)
                {
                    lblLowStock.Visible = qty < 5;
                    lblLowStock.Text = qty == 0
                        ? "⚠  OUT OF STOCK"
                        : "⚠  LOW STOCK — quantity below 5";
                    lblLowStock.ForeColor = qty == 0 ? C_AccRed : C_AccAmber;

                    lblStatusUpdate.ForeColor = C_AccGreen;
                    lblStatusUpdate.Text = $"✔  {cod} updated to {qty} pcs.";
                    LoadProducts();
                }
                else
                {
                    lblStatusUpdate.ForeColor = C_AccRed;
                    lblStatusUpdate.Text = "✘  Update failed. Check the product code.";
                }
            }
            else
            {
                lblStatusUpdate.ForeColor = C_AccRed;
                lblStatusUpdate.Text = "⚠  Enter a valid quantity (integer ≥ 0).";
            }
        }
    }
}