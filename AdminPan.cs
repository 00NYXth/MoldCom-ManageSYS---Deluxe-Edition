using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MoldCom
{
    public class AdminPan : Form
    {
        private Panel pnlSidebar, pnlContent;
        private Button btnManageProducts, btnManageUsers, btnReports, btnLogout;
        private Button _activeSidebarBtn;

        // ── Design System ──────────────────────────────────────────────
        private static readonly Color C_BgBase = Color.FromArgb(18, 24, 32);   // darkest bg
        private static readonly Color C_BgSurface = Color.FromArgb(26, 34, 46);   // sidebar/cards
        private static readonly Color C_BgPanel = Color.FromArgb(34, 44, 58);   // content bg
        private static readonly Color C_BgCard = Color.FromArgb(42, 54, 70);   // elevated cards
        private static readonly Color C_BgInput = Color.FromArgb(22, 30, 42);   // input fields
        private static readonly Color C_Border = Color.FromArgb(52, 68, 88);   // subtle borders
        private static readonly Color C_TextPrim = Color.FromArgb(230, 236, 244);
        private static readonly Color C_TextSec = Color.FromArgb(140, 160, 180);
        private static readonly Color C_AccBlue = Color.FromArgb(56, 132, 220);
        private static readonly Color C_AccGreen = Color.FromArgb(48, 168, 108);
        private static readonly Color C_AccRed = Color.FromArgb(196, 64, 72);
        private static readonly Color C_AccAmber = Color.FromArgb(204, 140, 40);
        private static readonly Color C_SideHover = Color.FromArgb(36, 48, 64);
        private static readonly Color C_SideActive = Color.FromArgb(56, 132, 220);

        private static readonly Font F_Title = new Font("Segoe UI", 13f, FontStyle.Bold);
        private static readonly Font F_SubHead = new Font("Segoe UI", 10f, FontStyle.Bold);
        private static readonly Font F_Body = new Font("Segoe UI", 9f);
        private static readonly Font F_Small = new Font("Segoe UI", 8f);
        private static readonly Font F_Tiny = new Font("Segoe UI", 7.5f);
        private static readonly Font F_Sidebar = new Font("Segoe UI", 9.5f);
        private static readonly Font F_Logo = new Font("Segoe UI", 11f, FontStyle.Bold);
        private static readonly Font F_Stat = new Font("Segoe UI", 20f, FontStyle.Bold);
        private static readonly Font F_StatLbl = new Font("Segoe UI", 7.5f);

        // Grid constants
        private const int SIDE_W = 220;
        private const int HEADER_H = 64;
        private const int PAD = 24;
        private const int GAP = 12;

        public AdminPan()
        {
            InitializeComponent();
            ShowDashboard();
        }

        // ════════════════════════════════════════════════════════════════
        //  SHELL
        // ════════════════════════════════════════════════════════════════
        private void InitializeComponent()
        {
            this.Text = "MoldCom — Admin Panel";
            this.Size = new Size(1280, 760);
            this.MinimumSize = new Size(1100, 660);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = C_BgPanel;
            this.ForeColor = C_TextPrim;

            // ── Sidebar ──────────────────────────────────────────────
            pnlSidebar = new Panel
            {
                BackColor = C_BgSurface,
                Dock = DockStyle.Left,
                Width = SIDE_W
            };

            // Top accent line
            Panel topLine = new Panel
            {
                BackColor = C_AccBlue,
                Dock = DockStyle.Top,
                Height = 3
            };

            // Logo block
            Panel pnlLogo = new Panel
            {
                BackColor = C_BgBase,
                Dock = DockStyle.Top,
                Height = 72
            };

            Label lblLogo1 = new Label
            {
                Text = "MOLDCOM",
                ForeColor = C_TextPrim,
                Font = F_Logo,
                Location = new Point(0, 18),
                Width = SIDE_W,
                TextAlign = ContentAlignment.MiddleCenter
            };
            Label lblLogo2 = new Label
            {
                Text = "ADMIN CONSOLE",
                ForeColor = C_AccBlue,
                Font = F_Tiny,
                Location = new Point(0, 42),
                Width = SIDE_W,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlLogo.Controls.AddRange(new Control[] { lblLogo1, lblLogo2 });

            // Divider after logo
            Panel divLogo = new Panel
            {
                BackColor = C_Border,
                Dock = DockStyle.Top,
                Height = 1
            };

            // Section label
            Label lblSection = new Label
            {
                Text = "NAVIGATION",
                ForeColor = C_TextSec,
                Font = new Font("Segoe UI", 7f, FontStyle.Bold),
                Location = new Point(20, 86),
                AutoSize = true
            };

            btnManageProducts = MakeSideBtn("📦   Products", 0);
            btnManageUsers = MakeSideBtn("👤   Users", 1);
            btnReports = MakeSideBtn("📊   Dashboard", 2);

            // Push logout to the bottom
            btnLogout = new Button
            {
                Text = "⬅   Logout",
                Location = new Point(14, 590),
                Size = new Size(SIDE_W - 28, 40),
                BackColor = Color.FromArgb(70, 30, 34),
                ForeColor = Color.FromArgb(220, 110, 118),
                FlatStyle = FlatStyle.Flat,
                Font = F_Sidebar,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 36, 40);

            btnManageProducts.Click += (s, e) => { SetActiveBtn(btnManageProducts); ShowProductManagement(); };
            btnManageUsers.Click += (s, e) => { SetActiveBtn(btnManageUsers); ShowUserManagement(); };
            btnReports.Click += (s, e) => { SetActiveBtn(btnReports); ShowDashboard(); };
            btnLogout.Click += (s, e) => this.Close();

            pnlSidebar.Controls.AddRange(new Control[] {
                topLine, pnlLogo, divLogo,
                lblSection,
                btnManageProducts, btnManageUsers, btnReports,
                btnLogout
            });

            // ── Content area ─────────────────────────────────────────
            pnlContent = new Panel
            {
                BackColor = C_BgPanel,
                Dock = DockStyle.Fill
            };

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlSidebar);
        }

        private Button MakeSideBtn(string text, int idx)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(14, 108 + idx * 52),
                Size = new Size(SIDE_W - 28, 42),
                BackColor = C_BgSurface,
                ForeColor = C_TextSec,
                FlatStyle = FlatStyle.Flat,
                Font = F_Sidebar,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = C_SideHover;
            return btn;
        }

        private void SetActiveBtn(Button btn)
        {
            if (_activeSidebarBtn != null)
            {
                _activeSidebarBtn.BackColor = C_BgSurface;
                _activeSidebarBtn.ForeColor = C_TextSec;
                _activeSidebarBtn.Font = F_Sidebar;
            }
            btn.BackColor = C_SideActive;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            _activeSidebarBtn = btn;
        }

        // ════════════════════════════════════════════════════════════════
        //  SHARED HEADER BAR
        // ════════════════════════════════════════════════════════════════
        private Panel MakeHeaderBar(string title, string subtitle = "")
        {
            var pnl = new Panel
            {
                BackColor = C_BgSurface,
                Dock = DockStyle.Top,
                Height = HEADER_H
            };

            // Bottom border
            Panel border = new Panel
            {
                BackColor = C_Border,
                Dock = DockStyle.Bottom,
                Height = 1
            };

            var lbl = new Label
            {
                Text = title,
                ForeColor = C_TextPrim,
                Font = F_Title,
                Location = new Point(PAD, subtitle == "" ? 20 : 12),
                AutoSize = true
            };

            pnl.Controls.AddRange(new Control[] { border, lbl });

            if (subtitle != "")
            {
                var sub = new Label
                {
                    Text = subtitle,
                    ForeColor = C_TextSec,
                    Font = F_Small,
                    Location = new Point(PAD, 36),
                    AutoSize = true
                };
                pnl.Controls.Add(sub);
            }

            return pnl;
        }

        // ════════════════════════════════════════════════════════════════
        //  DASHBOARD
        // ════════════════════════════════════════════════════════════════
        private void ShowDashboard()
        {
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(MakeHeaderBar("📊  Dashboard", "Warehouse overview and critical alerts"));

            var (totalUnice, totalBucati, valoare) = DatabaseHelper.GetRaportDepozit();
            var stocCritic = DatabaseHelper.GetProduseStocCritic();

            // ── Stat cards ───────────────────────────────────────────
            // 4 cards evenly spaced across the content width
            // Dynamic positioning on Resize
            Panel pnlCards = new Panel
            {
                Location = new Point(PAD, HEADER_H + PAD),
                Height = 100,
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            // We'll use a FlowLayoutPanel for automatic equal spacing
            FlowLayoutPanel flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent
            };

            flow.Controls.Add(MakeStatCard("UNIQUE PRODUCTS", totalUnice.ToString(), C_AccBlue, "items in catalog"));
            flow.Controls.Add(MakeStatCard("TOTAL STOCK UNITS", totalBucati + " pcs", C_AccGreen, "across all categories"));
            flow.Controls.Add(MakeStatCard("INVENTORY VALUE", $"{valoare:N0} MDL", C_AccAmber, "estimated total worth"));
            flow.Controls.Add(MakeStatCard("CRITICAL STOCK", stocCritic.Count.ToString(), C_AccRed, "products need attention"));

            pnlCards.Controls.Add(flow);

            // Dynamically size the FlowPanel cards when form resizes
            EventHandler resizeCards = (s, e2) =>
            {
                int availW = pnlContent.Width - PAD * 2;
                int cardW = (availW - GAP * 3) / 4;
                pnlCards.Width = availW;
                foreach (Control c in flow.Controls)
                    c.Width = Math.Max(cardW, 160);
            };
            pnlContent.Resize += resizeCards;
            resizeCards(null, null);

            // ── Section label ────────────────────────────────────────
            Label lblSec = new Label
            {
                Text = "⚠   CRITICAL STOCK ALERTS",
                ForeColor = C_AccAmber,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                Location = new Point(PAD, HEADER_H + PAD + 112),
                AutoSize = true
            };

            // Thin accent line next to section label
            Panel accentLine = new Panel
            {
                BackColor = C_AccAmber,
                Location = new Point(PAD, HEADER_H + PAD + 126),
                Size = new Size(60, 2)
            };

            // ── Critical-stock ListView ──────────────────────────────
            int listTop = HEADER_H + PAD + 136;
            ListView lvCritical = new ListView
            {
                Location = new Point(PAD, listTop),
                BackColor = C_BgCard,
                ForeColor = C_TextPrim,
                Font = F_Body,
                View = View.Details,
                FullRowSelect = true,
                GridLines = false,
                BorderStyle = BorderStyle.None,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            lvCritical.Columns.Add("Code", 100);
            lvCritical.Columns.Add("Product", 280);
            lvCritical.Columns.Add("Quantity", 90);
            lvCritical.Columns.Add("Status", 120);
            lvCritical.Columns.Add("Action", 160);

            Action setListSize = () =>
            {
                lvCritical.Width = pnlContent.Width - PAD * 2;
                lvCritical.Height = pnlContent.Height - listTop - PAD;
            };
            pnlContent.Resize += (s, e2) => setListSize();
            setListSize();

            if (stocCritic.Count == 0)
            {
                var ok = new ListViewItem("") { ForeColor = C_AccGreen };
                ok.SubItems.Add("✔  All products have sufficient stock — warehouse is healthy.");
                lvCritical.Items.Add(ok);
            }
            else
            {
                foreach (var p in stocCritic)
                {
                    bool oos = p.Cantitate == 0;
                    var lvi = new ListViewItem(p.Cod) { UseItemStyleForSubItems = false };
                    lvi.SubItems.Add(p.Nume);
                    var qtyItem = lvi.SubItems.Add(p.Cantitate.ToString());
                    qtyItem.ForeColor = oos ? C_AccRed : C_AccAmber;
                    var statusItem = lvi.SubItems.Add(oos ? "Out of Stock" : "Low Stock");
                    statusItem.ForeColor = oos ? C_AccRed : C_AccAmber;
                    lvi.SubItems.Add("Update stock →");
                    lvi.BackColor = oos
                        ? Color.FromArgb(50, 30, 32)
                        : Color.FromArgb(50, 42, 24);
                    lvi.ForeColor = C_TextPrim;
                    lvCritical.Items.Add(lvi);
                }
            }

            // Alternate row shading
            for (int i = 0; i < lvCritical.Items.Count; i++)
                if (lvCritical.Items[i].BackColor == C_BgCard || lvCritical.Items[i].BackColor == Color.Empty)
                    lvCritical.Items[i].BackColor = i % 2 == 0 ? C_BgCard : Color.FromArgb(38, 50, 64);

            pnlContent.Controls.AddRange(new Control[] {
                pnlCards, lblSec, accentLine, lvCritical });
        }

        private Panel MakeStatCard(string title, string value, Color accent, string hint)
        {
            var pnl = new Panel
            {
                Width = 200,
                Height = 96,
                BackColor = C_BgCard,
                Margin = new Padding(0, 0, GAP, 0),
                Cursor = Cursors.Default
            };

            // Left accent bar
            Panel bar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 4,
                BackColor = accent
            };

            Label lblVal = new Label
            {
                Text = value,
                ForeColor = Color.White,
                Font = F_Stat,
                Location = new Point(16, 14),
                AutoSize = true
            };

            Label lblTitle = new Label
            {
                Text = title,
                ForeColor = accent,
                Font = new Font("Segoe UI", 7.5f, FontStyle.Bold),
                Location = new Point(16, 56),
                AutoSize = true
            };

            Label lblHint = new Label
            {
                Text = hint,
                ForeColor = C_TextSec,
                Font = F_Tiny,
                Location = new Point(16, 74),
                AutoSize = true
            };

            pnl.Controls.AddRange(new Control[] { bar, lblVal, lblTitle, lblHint });
            return pnl;
        }

        // ════════════════════════════════════════════════════════════════
        //  PRODUCT MANAGEMENT
        // ════════════════════════════════════════════════════════════════
        private void ShowProductManagement()
        {
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(MakeHeaderBar("📦  Product Management", "Add, edit or remove products from inventory"));

            // ── Main layout: list LEFT | form RIGHT ─────────────────
            Panel pnlMain = new Panel
            {
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Location = new Point(PAD, HEADER_H + PAD)
            };

            Action sizeMain = () =>
            {
                pnlMain.Width = pnlContent.Width - PAD * 2;
                pnlMain.Height = pnlContent.Height - HEADER_H - PAD * 2;
            };
            pnlContent.Resize += (s, e) => sizeMain();
            sizeMain();

            // ── List panel ───────────────────────────────────────────
            Panel pnlList = new Panel
            {
                Location = new Point(0, 0),
                BackColor = C_BgCard,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom
            };

            Action sizeList = () =>
            {
                int listW = (int)(pnlMain.Width * 0.52);
                pnlList.Width = listW;
                pnlList.Height = pnlMain.Height;
            };
            pnlContent.Resize += (s, e) => sizeList();
            pnlMain.Resize += (s, e) => sizeList();
            sizeList();

            // Search bar inside list panel
            Panel pnlSearch = new Panel
            {
                BackColor = C_BgSurface,
                Dock = DockStyle.Top,
                Height = 48
            };

            TextBox txtSearch = new TextBox
            {
                Location = new Point(12, 12),
                Width = 220,
                BackColor = C_BgInput,
                ForeColor = C_TextPrim,
                BorderStyle = BorderStyle.FixedSingle,
                Font = F_Body
            };
            txtSearch.Text = "Search products…";
            txtSearch.ForeColor = C_TextSec;
            txtSearch.GotFocus += (s, e) => { if (txtSearch.ForeColor == C_TextSec) { txtSearch.Text = ""; txtSearch.ForeColor = C_TextPrim; } };
            txtSearch.LostFocus += (s, e) => { if (string.IsNullOrEmpty(txtSearch.Text)) { txtSearch.Text = "Search products…"; txtSearch.ForeColor = C_TextSec; } };

            Button btnSearchGo = MakeCompactBtn("Search", C_AccBlue, 240, 12, 80);
            Button btnReset = MakeCompactBtn("Reset", C_BgInput, 328, 12, 70);

            pnlSearch.Controls.AddRange(new Control[] { txtSearch, btnSearchGo, btnReset });

            ListView lvProducts = new ListView
            {
                Dock = DockStyle.Fill,
                BackColor = C_BgCard,
                ForeColor = C_TextPrim,
                Font = F_Body,
                View = View.Details,
                FullRowSelect = true,
                GridLines = false,
                BorderStyle = BorderStyle.None
            };
            lvProducts.Columns.AddRange(new ColumnHeader[] {
                new ColumnHeader { Text = "Code",      Width = 80  },
                new ColumnHeader { Text = "Name",      Width = 160 },
                new ColumnHeader { Text = "Category",  Width = 100 },
                new ColumnHeader { Text = "Price",     Width = 90  },
                new ColumnHeader { Text = "Qty",       Width = 55  },
                new ColumnHeader { Text = "Location",  Width = 90  }
            });

            pnlList.Controls.AddRange(new Control[] { lvProducts, pnlSearch });

            // ── Form panel ───────────────────────────────────────────
            Panel pnlForm = new Panel
            {
                BackColor = C_BgSurface,
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom
            };

            Action sizeForm = () =>
            {
                int listW = (int)(pnlMain.Width * 0.52);
                pnlForm.Location = new Point(listW + GAP, 0);
                pnlForm.Width = pnlMain.Width - listW - GAP;
                pnlForm.Height = pnlMain.Height;
            };
            pnlContent.Resize += (s, e) => sizeForm();
            pnlMain.Resize += (s, e) => sizeForm();
            sizeForm();

            // Form header
            Panel frmHeader = new Panel
            {
                BackColor = C_BgBase,
                Dock = DockStyle.Top,
                Height = 48
            };
            Label lblFormTitle = new Label
            {
                Text = "Product Details",
                ForeColor = C_TextPrim,
                Font = F_SubHead,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(16, 0, 0, 0)
            };
            Panel frmHeaderLine = new Panel { BackColor = C_Border, Dock = DockStyle.Bottom, Height = 1 };
            frmHeader.Controls.AddRange(new Control[] { lblFormTitle, frmHeaderLine });

            // Form fields — all inside a scrollable inner panel
            Panel frmBody = new Panel
            {
                Location = new Point(0, 48),
                BackColor = C_BgSurface,
                AutoScroll = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            Action sizeFrmBody = () =>
            {
                frmBody.Width = pnlForm.Width;
                frmBody.Height = pnlForm.Height - 48;
            };
            pnlForm.Resize += (s, e) => sizeFrmBody();
            sizeFrmBody();

            // ── Field factory with label + input pairs ────────────────
            // Each row: y position managed manually with consistent 64px row height
            const int FX = 20;   // field x
            int FW() => Math.Max(frmBody.Width - FX * 2, 180);  // field width dynamic

            Label MkLbl(string t, int y) => new Label { Text = t, ForeColor = C_TextSec, Font = F_Small, Location = new Point(FX, y), AutoSize = true };
            TextBox MkTxt(int y, int w = 0) => new TextBox
            {
                Location = new Point(FX, y),
                Width = w > 0 ? w : FW(),
                Height = 26,
                BackColor = C_BgInput,
                ForeColor = C_TextPrim,
                BorderStyle = BorderStyle.FixedSingle,
                Font = F_Body
            };

            // Row 1: Code (half) | Location (half)
            var lblCode = MkLbl("PRODUCT CODE *", 16);
            var txtCode = MkTxt(32, 140);

            var lblLoc = MkLbl("LOCATION", 16);
            var txtLoc = MkTxt(32, 140);

            // Row 2: Name (full)
            var lblName = MkLbl("PRODUCT NAME *", 80);
            var txtName = MkTxt(96);

            // Row 3: Category (half) | Price (quarter) | Qty (quarter)
            var lblCat = MkLbl("CATEGORY", 148);
            var txtCat = MkTxt(164, 160);
            var lblPrice = MkLbl("PRICE (MDL) *", 148);
            var txtPrice = MkTxt(164, 100);
            var lblQty = MkLbl("QUANTITY *", 148);
            var txtQty = MkTxt(164, 80);

            // Row 4: Description (full, multiline)
            var lblDesc = MkLbl("DESCRIPTION", 216);
            var txtDesc = new TextBox
            {
                Location = new Point(FX, 232),
                BackColor = C_BgInput,
                ForeColor = C_TextPrim,
                BorderStyle = BorderStyle.FixedSingle,
                Font = F_Body,
                Multiline = true,
                Height = 72,
                ScrollBars = ScrollBars.Vertical
            };

            // Position the half-row fields dynamically
            Action layoutFormFields = () =>
            {
                int fw = Math.Max(frmBody.Width - FX * 2, 180);
                int half = (fw - GAP) / 2;
                int qtr = (fw - GAP * 2) / 4;

                txtCode.Width = half;
                txtLoc.Location = new Point(FX + half + GAP, 32);
                lblLoc.Location = new Point(FX + half + GAP, 16);
                txtLoc.Width = half;

                txtName.Width = fw;

                int catW = fw - qtr * 2 - GAP * 2;
                txtCat.Width = catW;
                lblPrice.Location = new Point(FX + catW + GAP, 148);
                txtPrice.Location = new Point(FX + catW + GAP, 164);
                txtPrice.Width = qtr;
                lblQty.Location = new Point(FX + catW + GAP + qtr + GAP, 148);
                txtQty.Location = new Point(FX + catW + GAP + qtr + GAP, 164);
                txtQty.Width = qtr;

                txtDesc.Width = fw;
            };
            frmBody.Resize += (s, e) => layoutFormFields();
            layoutFormFields();

            // Status label
            Label lblStatus = new Label
            {
                Text = "",
                ForeColor = C_AccGreen,
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                Location = new Point(FX, 318),
                AutoSize = true
            };

            // Action buttons — symmetric row
            Panel pnlBtns = new Panel
            {
                Location = new Point(0, 340),
                Height = 100,
                BackColor = Color.Transparent
            };
            Action sizeBtnPanel = () => pnlBtns.Width = frmBody.Width;
            frmBody.Resize += (s, e) => sizeBtnPanel();
            sizeBtnPanel();

            Button btnAdd = MakeFormBtn("Add Product", C_AccGreen, 0);
            Button btnUpdate = MakeFormBtn("Update", C_AccBlue, 1);
            Button btnDelete = MakeFormBtn("Delete", C_AccRed, 2);
            Button btnClear = MakeFormBtn("Clear Fields", C_BgInput, 3);

            // Dynamic equal-width buttons
            Action sizeFormBtns = () =>
            {
                int availW = frmBody.Width - FX * 2;
                int bw = (availW - GAP * 3) / 4;
                int bh = 36;
                int by = 8;
                btnAdd.Location = new Point(FX, by); btnAdd.Width = bw;
                btnUpdate.Location = new Point(FX + (bw + GAP), by); btnUpdate.Width = bw;
                btnDelete.Location = new Point(FX + (bw + GAP) * 2, by); btnDelete.Width = bw;
                btnClear.Location = new Point(FX + (bw + GAP) * 3, by); btnClear.Width = bw;
                foreach (Button b in new[] { btnAdd, btnUpdate, btnDelete, btnClear }) b.Height = bh;
            };
            frmBody.Resize += (s, e) => sizeFormBtns();
            sizeFormBtns();
            pnlBtns.Controls.AddRange(new Control[] { btnAdd, btnUpdate, btnDelete, btnClear });

            frmBody.Controls.AddRange(new Control[] {
                lblCode, txtCode, lblLoc, txtLoc,
                lblName, txtName,
                lblCat, txtCat, lblPrice, txtPrice, lblQty, txtQty,
                lblDesc, txtDesc,
                lblStatus, pnlBtns
            });

            pnlForm.Controls.AddRange(new Control[] { frmHeader, frmBody });

            // ── Data ops ─────────────────────────────────────────────
            Action RefreshGrid = () =>
            {
                lvProducts.Items.Clear();
                foreach (var p in DatabaseHelper.GetAllProduse())
                {
                    var lvi = new ListViewItem(p.Cod);
                    lvi.SubItems.Add(p.Nume);
                    lvi.SubItems.Add(p.Categorie);
                    lvi.SubItems.Add(p.Pret.ToString("0.00"));
                    lvi.SubItems.Add(p.Cantitate.ToString());
                    lvi.SubItems.Add(p.Locatie);
                    lvi.Tag = p;
                    if (p.Cantitate == 0) lvi.BackColor = Color.FromArgb(50, 28, 30);
                    else if (p.Cantitate < 5) lvi.BackColor = Color.FromArgb(50, 42, 24);
                    lvProducts.Items.Add(lvi);
                }
                // Alternate shading
                for (int i = 0; i < lvProducts.Items.Count; i++)
                    if (lvProducts.Items[i].BackColor == C_BgCard || lvProducts.Items[i].BackColor == Color.Empty)
                        lvProducts.Items[i].BackColor = i % 2 == 0 ? C_BgCard : Color.FromArgb(38, 50, 64);
            };
            RefreshGrid();

            Action<string> ShowOk = msg => { lblStatus.ForeColor = C_AccGreen; lblStatus.Text = "✔  " + msg; };
            Action<string> ShowErr = msg => { lblStatus.ForeColor = C_AccRed; lblStatus.Text = "⚠  " + msg; };

            Action ClearForm = () =>
            {
                foreach (var tb in new[] { txtCode, txtName, txtCat, txtPrice, txtQty, txtLoc, txtDesc })
                { tb.Clear(); tb.ReadOnly = false; }
                lblStatus.Text = "";
            };

            lvProducts.SelectedIndexChanged += (s, e) =>
            {
                if (lvProducts.SelectedItems.Count > 0 && lvProducts.SelectedItems[0].Tag is Produs p)
                {
                    txtCode.Text = p.Cod; txtCode.ReadOnly = true;
                    txtName.Text = p.Nume; txtCat.Text = p.Categorie;
                    txtPrice.Text = p.Pret.ToString(); txtQty.Text = p.Cantitate.ToString();
                    txtLoc.Text = p.Locatie; txtDesc.Text = p.Descriere;
                    lblStatus.Text = "";
                }
            };

            btnSearchGo.Click += (s, e) =>
            {
                string t = txtSearch.Text.Trim();
                if (string.IsNullOrEmpty(t) || t == "Search products…") { RefreshGrid(); return; }
                lvProducts.Items.Clear();
                foreach (var p in DatabaseHelper.SearchProduse(t))
                {
                    var lvi = new ListViewItem(p.Cod);
                    lvi.SubItems.Add(p.Nume); lvi.SubItems.Add(p.Categorie);
                    lvi.SubItems.Add(p.Pret.ToString("0.00")); lvi.SubItems.Add(p.Cantitate.ToString());
                    lvi.SubItems.Add(p.Locatie); lvi.Tag = p;
                    lvProducts.Items.Add(lvi);
                }
            };
            btnReset.Click += (s, e) => { txtSearch.Text = "Search products…"; txtSearch.ForeColor = C_TextSec; RefreshGrid(); };
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) btnSearchGo.PerformClick(); };

            btnAdd.Click += (s, e) =>
            {
                if (!ValidForm(txtCode, txtName, txtPrice, txtQty, ShowErr)) return;
                txtCode.ReadOnly = false;
                if (DatabaseHelper.AddProdus(Build(txtCode, txtName, txtCat, txtPrice, txtQty, txtLoc, txtDesc)))
                { RefreshGrid(); ShowOk("Product added successfully."); }
            };

            btnUpdate.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtCode.Text)) { ShowErr("Select a product first."); return; }
                if (!ValidForm(txtCode, txtName, txtPrice, txtQty, ShowErr)) return;
                txtCode.ReadOnly = false;
                if (DatabaseHelper.UpdateProdus(Build(txtCode, txtName, txtCat, txtPrice, txtQty, txtLoc, txtDesc)))
                { RefreshGrid(); ShowOk("Product updated."); }
            };

            btnDelete.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtCode.Text)) { ShowErr("Select a product first."); return; }
                if (MessageBox.Show($"Delete \"{txtName.Text}\"?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (DatabaseHelper.DeleteProdus(txtCode.Text))
                    { RefreshGrid(); ClearForm(); ShowOk("Product deleted."); }
                }
            };

            btnClear.Click += (s, e) => ClearForm();

            pnlMain.Controls.AddRange(new Control[] { pnlList, pnlForm });
            pnlContent.Controls.Add(pnlMain);
        }

        // ════════════════════════════════════════════════════════════════
        //  USER MANAGEMENT
        // ════════════════════════════════════════════════════════════════
        private void ShowUserManagement()
        {
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(MakeHeaderBar("👤  User Management", "Manage accounts and access roles"));

            Panel pnlMain = new Panel
            {
                BackColor = Color.Transparent,
                Location = new Point(PAD, HEADER_H + PAD),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            Action sizeMain = () => { pnlMain.Width = pnlContent.Width - PAD * 2; pnlMain.Height = pnlContent.Height - HEADER_H - PAD * 2; };
            pnlContent.Resize += (s, e) => sizeMain(); sizeMain();

            // ── List ─────────────────────────────────────────────────
            Panel pnlList = new Panel { Location = new Point(0, 0), BackColor = C_BgCard, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom };
            Action sizeList = () => { pnlList.Width = (int)(pnlMain.Width * 0.48); pnlList.Height = pnlMain.Height; };
            pnlContent.Resize += (s, e) => sizeList(); pnlMain.Resize += (s, e) => sizeList(); sizeList();

            ListView lvUsers = new ListView
            {
                Dock = DockStyle.Fill,
                BackColor = C_BgCard,
                ForeColor = C_TextPrim,
                Font = F_Body,
                View = View.Details,
                FullRowSelect = true,
                GridLines = false,
                BorderStyle = BorderStyle.None
            };
            lvUsers.Columns.AddRange(new ColumnHeader[] {
                new ColumnHeader { Text = "ID",       Width = 45  },
                new ColumnHeader { Text = "Username", Width = 160 },
                new ColumnHeader { Text = "Password", Width = 120 },
                new ColumnHeader { Text = "Role",     Width = 110 }
            });
            pnlList.Controls.Add(lvUsers);

            // ── Form ─────────────────────────────────────────────────
            Panel pnlForm = new Panel { BackColor = C_BgSurface, Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom };
            Action sizeForm = () =>
            {
                int lw = (int)(pnlMain.Width * 0.48);
                pnlForm.Location = new Point(lw + GAP, 0);
                pnlForm.Width = pnlMain.Width - lw - GAP;
                pnlForm.Height = pnlMain.Height;
            };
            pnlContent.Resize += (s, e) => sizeForm(); pnlMain.Resize += (s, e) => sizeForm(); sizeForm();

            Panel frmHeader = new Panel { BackColor = C_BgBase, Dock = DockStyle.Top, Height = 48 };
            Label lblFT = new Label { Text = "User Details", ForeColor = C_TextPrim, Font = F_SubHead, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(16, 0, 0, 0) };
            frmHeader.Controls.Add(lblFT);

            const int FX = 20;
            int FW2() => Math.Max(pnlForm.Width - FX * 2, 160);

            TextBox MkTxt(int y) => new TextBox { Location = new Point(FX, y), BackColor = C_BgInput, ForeColor = C_TextPrim, BorderStyle = BorderStyle.FixedSingle, Font = F_Body, Height = 26 };
            Label MkLbl(string t, int y) => new Label { Text = t, ForeColor = C_TextSec, Font = F_Small, Location = new Point(FX, y), AutoSize = true };

            var lblUser = MkLbl("USERNAME *", 64); var txtUser = MkTxt(80);
            var lblPass = MkLbl("PASSWORD", 120); var txtPass = MkTxt(136); txtPass.PasswordChar = '●';
            var lblRole = MkLbl("ROLE *", 180);
            ComboBox cmbRole = new ComboBox
            {
                Location = new Point(FX, 196),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = C_BgInput,
                ForeColor = C_TextPrim,
                FlatStyle = FlatStyle.Flat,
                Font = F_Body
            };
            cmbRole.Items.AddRange(new string[] { "Admin", "Manager", "Casier", "Operator", "Vizitator" });

            Label lblStatus = new Label { Text = "", ForeColor = C_AccGreen, Font = new Font("Segoe UI", 8f, FontStyle.Bold), Location = new Point(FX, 256), AutoSize = true };

            Panel pnlBtns = new Panel { Location = new Point(0, 276), Height = 50, BackColor = Color.Transparent };

            Button btnAddU = MakeFormBtn("Add User", C_AccGreen, 0);
            Button btnUpdateU = MakeFormBtn("Update", C_AccBlue, 1);
            Button btnDeleteU = MakeFormBtn("Delete", C_AccRed, 2);
            Button btnClearU = MakeFormBtn("Clear", C_BgInput, 3);

            Action sizeBtns = () =>
            {
                int aw = pnlForm.Width - FX * 2;
                int bw = (aw - GAP * 3) / 4;
                int bh = 36;
                btnAddU.Location = new Point(FX, 8); btnAddU.Width = bw; btnAddU.Height = bh;
                btnUpdateU.Location = new Point(FX + (bw + GAP), 8); btnUpdateU.Width = bw; btnUpdateU.Height = bh;
                btnDeleteU.Location = new Point(FX + (bw + GAP) * 2, 8); btnDeleteU.Width = bw; btnDeleteU.Height = bh;
                btnClearU.Location = new Point(FX + (bw + GAP) * 3, 8); btnClearU.Width = bw; btnClearU.Height = bh;
            };
            pnlForm.Resize += (s, e) =>
            {
                int fw = FW2();
                txtUser.Width = fw; txtPass.Width = fw; cmbRole.Width = fw;
                pnlBtns.Width = pnlForm.Width;
                sizeBtns();
            };
            txtUser.Width = FW2(); txtPass.Width = FW2(); cmbRole.Width = FW2();
            pnlBtns.Width = pnlForm.Width; sizeBtns();
            pnlBtns.Controls.AddRange(new Control[] { btnAddU, btnUpdateU, btnDeleteU, btnClearU });

            pnlForm.Controls.AddRange(new Control[] {
                frmHeader, lblUser, txtUser, lblPass, txtPass, lblRole, cmbRole,
                lblStatus, pnlBtns
            });

            // ── Data ops ─────────────────────────────────────────────
            Action RefreshUsers = () =>
            {
                lvUsers.Items.Clear();
                foreach (var u in DatabaseHelper.GetAllUtilizatori())
                {
                    var lvi = new ListViewItem(u.ID.ToString());
                    lvi.SubItems.Add(u.Username); lvi.SubItems.Add(u.Password); lvi.SubItems.Add(u.Rol);
                    lvi.Tag = u;
                    lvUsers.Items.Add(lvi);
                }
                for (int i = 0; i < lvUsers.Items.Count; i++)
                    lvUsers.Items[i].BackColor = i % 2 == 0 ? C_BgCard : Color.FromArgb(38, 50, 64);
            };
            RefreshUsers();

            Action<string> ShowOk = msg => { lblStatus.ForeColor = C_AccGreen; lblStatus.Text = "✔  " + msg; };
            Action<string> ShowErr = msg => { lblStatus.ForeColor = C_AccRed; lblStatus.Text = "⚠  " + msg; };

            lvUsers.SelectedIndexChanged += (s, e) =>
            {
                if (lvUsers.SelectedItems.Count > 0 && lvUsers.SelectedItems[0].Tag is USER u)
                { txtUser.Text = u.Username; txtPass.Text = u.Password; cmbRole.SelectedItem = u.Rol; lblStatus.Text = ""; }
            };

            btnAddU.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtUser.Text)) { ShowErr("Username is required."); return; }
                if (cmbRole.SelectedItem == null) { ShowErr("Select a role."); return; }
                if (DatabaseHelper.AddUtilizator(new USER { Username = txtUser.Text, Password = txtPass.Text, Rol = cmbRole.SelectedItem.ToString() }))
                { RefreshUsers(); ShowOk($"User '{txtUser.Text}' added."); }
            };

            btnUpdateU.Click += (s, e) =>
            {
                if (lvUsers.SelectedItems.Count == 0) { ShowErr("Select a user first."); return; }
                if (lvUsers.SelectedItems[0].Tag is USER u)
                {
                    u.Username = txtUser.Text; u.Password = txtPass.Text;
                    if (cmbRole.SelectedItem != null) u.Rol = cmbRole.SelectedItem.ToString();
                    if (DatabaseHelper.UpdateUtilizator(u)) { RefreshUsers(); ShowOk("User updated."); }
                }
            };

            btnDeleteU.Click += (s, e) =>
            {
                if (lvUsers.SelectedItems.Count == 0) { ShowErr("Select a user first."); return; }
                if (lvUsers.SelectedItems[0].Tag is USER u)
                    if (MessageBox.Show($"Delete '{u.Username}'?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        if (DatabaseHelper.DeleteUtilizator(u.ID)) { RefreshUsers(); txtUser.Clear(); txtPass.Clear(); cmbRole.SelectedIndex = -1; ShowOk("User deleted."); }
            };

            btnClearU.Click += (s, e) => { txtUser.Clear(); txtPass.Clear(); cmbRole.SelectedIndex = -1; lblStatus.Text = ""; };

            pnlMain.Controls.AddRange(new Control[] { pnlList, pnlForm });
            pnlContent.Controls.Add(pnlMain);
        }

        // ════════════════════════════════════════════════════════════════
        //  SHARED HELPERS
        // ════════════════════════════════════════════════════════════════
        private bool ValidForm(TextBox code, TextBox name, TextBox price, TextBox qty, Action<string> err)
        {
            if (string.IsNullOrWhiteSpace(code.Text)) { err("Product code is required."); return false; }
            if (string.IsNullOrWhiteSpace(name.Text)) { err("Product name is required."); return false; }
            if (!decimal.TryParse(price.Text, out _)) { err("Price must be a valid number."); return false; }
            if (!int.TryParse(qty.Text, out int q) || q < 0) { err("Quantity must be a whole number ≥ 0."); return false; }
            return true;
        }

        private Produs Build(TextBox cod, TextBox nume, TextBox cat, TextBox pret, TextBox qty, TextBox loc, TextBox desc)
            => new Produs
            {
                Cod = cod.Text.Trim(),
                Nume = nume.Text.Trim(),
                Categorie = cat.Text.Trim(),
                Pret = decimal.TryParse(pret.Text, out var p) ? p : 0,
                Cantitate = int.TryParse(qty.Text, out var q) ? q : 0,
                Locatie = loc.Text.Trim(),
                Descriere = desc.Text.Trim()
            };

        private Button MakeCompactBtn(string text, Color bg, int x, int y, int w)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, 26),
                BackColor = bg,
                ForeColor = C_TextPrim,
                FlatStyle = FlatStyle.Flat,
                Font = F_Small,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private Button MakeFormBtn(string text, Color bg, int placeholder)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(100, 36),
                BackColor = bg,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = F_Body,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
    }
}