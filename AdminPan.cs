using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MoldCom
{
    public class AdminPan : Form
    {
        private Panel pnlSidebar, pnlContent;
        private Button btnManageProducts, btnManageUsers, btnReports, btnLogout;

        public AdminPan()
        {
            InitializeComponent();
            ShowProductManagement();
        }

        private void InitializeComponent()
        {
            this.Text = "Admin Panel  [V2]";
            this.Size = new System.Drawing.Size(1200, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(44, 55, 68);

            pnlSidebar = new Panel();
            pnlSidebar.BackColor = System.Drawing.Color.FromArgb(33, 44, 57);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Width = 200;

            Label lblAdmin = new Label();
            lblAdmin.Text = "ADMIN PANEL";
            lblAdmin.ForeColor = System.Drawing.Color.White;
            lblAdmin.Font = new System.Drawing.Font("Arial", 13, System.Drawing.FontStyle.Bold);
            lblAdmin.AutoSize = false;
            lblAdmin.Width = 200;
            lblAdmin.Height = 60;
            lblAdmin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblAdmin.Location = new System.Drawing.Point(0, 20);

            btnManageProducts = CreateSidebarButton("Manage Products", 100);
            btnManageUsers = CreateSidebarButton("Manage Users", 160);
            btnReports = CreateSidebarButton("Reports", 220);
            btnLogout = CreateSidebarButton("Logout", 280);

            btnManageProducts.Click += (s, e) => ShowProductManagement();
            btnManageUsers.Click += (s, e) => ShowUserManagement();
            btnReports.Click += (s, e) => ShowReports();
            btnLogout.Click += (s, e) => this.Close();

            pnlSidebar.Controls.AddRange(new Control[] {
                lblAdmin, btnManageProducts, btnManageUsers, btnReports, btnLogout });

            pnlContent = new Panel();
            pnlContent.BackColor = System.Drawing.Color.FromArgb(44, 55, 68);
            pnlContent.Dock = DockStyle.Fill;

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlSidebar);
        }

        private Button CreateSidebarButton(string text, int y)
        {
            var btn = new Button();
            btn.Text = text;
            btn.Location = new System.Drawing.Point(20, y);
            btn.Size = new System.Drawing.Size(160, 45);
            btn.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);
            btn.ForeColor = System.Drawing.Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new System.Drawing.Font("Arial", 10);
            return btn;
        }

        // ════════════════════════════════════════════════
        //  PRODUCT MANAGEMENT
        // ════════════════════════════════════════════════
        private void ShowProductManagement()
        {
            pnlContent.Controls.Clear();

            Panel pnlTitleBar = MakeTitleBar("Product Management");

            ListView lvProducts = new ListView();
            lvProducts.Location = new System.Drawing.Point(20, 80);
            lvProducts.Size = new System.Drawing.Size(570, 480);
            lvProducts.View = View.Details;
            lvProducts.FullRowSelect = true;
            lvProducts.GridLines = true;
            lvProducts.BackColor = System.Drawing.Color.FromArgb(33, 44, 57);
            lvProducts.ForeColor = System.Drawing.Color.White;
            lvProducts.Columns.AddRange(new ColumnHeader[] {
                new ColumnHeader { Text = "Cod",        Width = 70  },
                new ColumnHeader { Text = "Nume",       Width = 150 },
                new ColumnHeader { Text = "Categorie",  Width = 100 },
                new ColumnHeader { Text = "Pret (MDL)", Width = 80  },
                new ColumnHeader { Text = "Cantitate",  Width = 70  },
                new ColumnHeader { Text = "Locație",    Width = 90  }
            });

            // ── sp_GetAllProduse ──
            Action RefreshGrid = () =>
            {
                lvProducts.Items.Clear();
                foreach (var p in DatabaseHelper.GetAllProduse())
                {
                    var item = new ListViewItem(p.Cod);
                    item.SubItems.Add(p.Nume);
                    item.SubItems.Add(p.Categorie);
                    item.SubItems.Add(p.Pret.ToString("0.00"));
                    item.SubItems.Add(p.Cantitate.ToString());
                    item.SubItems.Add(p.Locatie);
                    item.Tag = p;
                    lvProducts.Items.Add(item);
                }
            };
            RefreshGrid();

            // Câmpuri formular dreapta
            Label lblCode = CreateLabel("Product Code", 100, 620);
            TextBox txtCode = CreateTextBox(80, 760, "txtCode");

            Label lblName = CreateLabel("Product Name", 135, 620);
            TextBox txtName = CreateTextBox(115, 760, "txtName");

            Label lblCat = CreateLabel("Category", 170, 620);
            TextBox txtCat = CreateTextBox(150, 760, "txtCategory");

            Label lblPrice = CreateLabel("Price (MDL)", 205, 620);
            TextBox txtPrice = CreateTextBox(185, 760, "txtPrice");

            Label lblQty = CreateLabel("Quantity", 240, 620);
            TextBox txtQty = CreateTextBox(220, 760, "txtQuantity");

            Label lblLoc = CreateLabel("Location", 275, 620);
            TextBox txtLoc = CreateTextBox(255, 760, "txtLocation");

            Label lblDesc = CreateLabel("Descriere", 310, 620);
            TextBox txtDesc = CreateTextBox(290, 760, "txtDesc");

            // Selectare din grilă → populează formularul
            lvProducts.SelectedIndexChanged += (s, e) =>
            {
                if (lvProducts.SelectedItems.Count > 0 &&
                    lvProducts.SelectedItems[0].Tag is Produs p)
                {
                    txtCode.Text = p.Cod;
                    txtName.Text = p.Nume;
                    txtCat.Text = p.Categorie;
                    txtPrice.Text = p.Pret.ToString();
                    txtQty.Text = p.Cantitate.ToString();
                    txtLoc.Text = p.Locatie;
                    txtDesc.Text = p.Descriere;
                    txtCode.ReadOnly = true; // codul nu se schimbă la update
                }
            };

            Button btnAdd = CreateButton("Add Product", 80, 620);
            Button btnUpdate = CreateButton("Update Product", 80, 770);
            Button btnDelete = CreateButton("Delete Product", 125, 620);
            Button btnSearch = CreateButton("Search by Name", 125, 770);

            // ── sp_AddProdus ──
            btnAdd.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtCode.Text) ||
                    string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Codul și Numele sunt obligatorii!", "Eroare",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                }
                txtCode.ReadOnly = false;
                var p = BuildProdus(txtCode, txtName, txtCat, txtPrice, txtQty, txtLoc, txtDesc);
                if (DatabaseHelper.AddProdus(p))
                {
                    RefreshGrid();
                    MessageBox.Show("Produs adăugat cu succes!");
                }
            };

            // ── sp_UpdateProdus ──
            btnUpdate.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtCode.Text)) { MessageBox.Show("Selectați un produs!"); return; }
                txtCode.ReadOnly = false;
                var p = BuildProdus(txtCode, txtName, txtCat, txtPrice, txtQty, txtLoc, txtDesc);
                if (DatabaseHelper.UpdateProdus(p))
                {
                    RefreshGrid();
                    MessageBox.Show("Produs actualizat cu succes!");
                }
            };

            // ── sp_DeleteProdus ──
            btnDelete.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtCode.Text)) { MessageBox.Show("Selectați un produs!"); return; }
                if (MessageBox.Show($"Ștergi produsul {txtName.Text}?", "Confirmare",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (DatabaseHelper.DeleteProdus(txtCode.Text))
                    {
                        RefreshGrid();
                        ClearProductFields(txtCode, txtName, txtCat, txtPrice, txtQty, txtLoc, txtDesc);
                        txtCode.ReadOnly = false;
                    }
                }
            };

            // ── sp_SearchProduse ──
            btnSearch.Click += (s, e) =>
            {
                string term = txtName.Text.Trim();
                if (string.IsNullOrEmpty(term)) { RefreshGrid(); return; }
                lvProducts.Items.Clear();
                foreach (var p in DatabaseHelper.SearchProduse(term))
                {
                    var item = new ListViewItem(p.Cod);
                    item.SubItems.Add(p.Nume);
                    item.SubItems.Add(p.Categorie);
                    item.SubItems.Add(p.Pret.ToString("0.00"));
                    item.SubItems.Add(p.Cantitate.ToString());
                    item.SubItems.Add(p.Locatie);
                    item.Tag = p;
                    lvProducts.Items.Add(item);
                }
            };

            pnlContent.Controls.AddRange(new Control[] {
                pnlTitleBar, lvProducts,
                lblCode, txtCode, lblName, txtName, lblCat, txtCat,
                lblPrice, txtPrice, lblQty, txtQty, lblLoc, txtLoc,
                lblDesc, txtDesc,
                btnAdd, btnUpdate, btnDelete, btnSearch });
        }

        // ════════════════════════════════════════════════
        //  USER MANAGEMENT
        // ════════════════════════════════════════════════
        private void ShowUserManagement()
        {
            pnlContent.Controls.Clear();

            Panel pnlTitleBar = MakeTitleBar("User Management");

            ListView lvUsers = new ListView();
            lvUsers.Location = new System.Drawing.Point(20, 80);
            lvUsers.Size = new System.Drawing.Size(570, 480);
            lvUsers.View = View.Details;
            lvUsers.FullRowSelect = true;
            lvUsers.GridLines = true;
            lvUsers.BackColor = System.Drawing.Color.FromArgb(33, 44, 57);
            lvUsers.ForeColor = System.Drawing.Color.White;
            lvUsers.Columns.AddRange(new ColumnHeader[] {
                new ColumnHeader { Text = "ID",       Width = 50  },
                new ColumnHeader { Text = "Username", Width = 200 },
                new ColumnHeader { Text = "Password", Width = 150 },
                new ColumnHeader { Text = "Role",     Width = 150 }
            });

            // ── sp_GetAllUtilizatori ──
            Action RefreshUsers = () =>
            {
                lvUsers.Items.Clear();
                foreach (var u in DatabaseHelper.GetAllUtilizatori())
                {
                    var item = new ListViewItem(u.ID.ToString());
                    item.SubItems.Add(u.Username);
                    item.SubItems.Add(u.Password);
                    item.SubItems.Add(u.Rol);
                    item.Tag = u;
                    lvUsers.Items.Add(item);
                }
            };
            RefreshUsers();

            Label lblUser = CreateLabel("Username", 100, 620);
            TextBox txtUser = CreateTextBox(80, 760, "txtUsr");

            Label lblPass = CreateLabel("Password", 135, 620);
            TextBox txtPass = CreateTextBox(115, 760, "txtPass");

            Label lblRole = CreateLabel("Role", 170, 620);
            ComboBox cmbRole = new ComboBox();
            cmbRole.Location = new System.Drawing.Point(760, 188);
            cmbRole.Width = 200;
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.Items.AddRange(new string[] { "Admin", "Employee", "Client" });

            lvUsers.SelectedIndexChanged += (s, e) =>
            {
                if (lvUsers.SelectedItems.Count > 0 &&
                    lvUsers.SelectedItems[0].Tag is USER u)
                {
                    txtUser.Text = u.Username;
                    txtPass.Text = u.Password;
                    cmbRole.SelectedItem = u.Rol;
                }
            };

            Button btnAddUser = CreateButton("Add User", 80, 620);
            Button btnUpdateUser = CreateButton("Update User", 80, 770);
            Button btnDeleteUser = CreateButton("Delete User", 125, 620);

            // ── sp_AddUtilizator ──
            btnAddUser.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtUser.Text) || cmbRole.SelectedItem == null)
                { MessageBox.Show("Username și rolul sunt obligatorii!"); return; }

                if (DatabaseHelper.AddUtilizator(new USER
                {
                    Username = txtUser.Text,
                    Password = txtPass.Text,
                    Rol = cmbRole.SelectedItem.ToString()
                }))
                {
                    RefreshUsers();
                    MessageBox.Show("Utilizator adăugat cu succes!");
                }
            };

            // ── sp_UpdateUtilizator ──
            btnUpdateUser.Click += (s, e) =>
            {
                if (lvUsers.SelectedItems.Count == 0) { MessageBox.Show("Selectați un utilizator!"); return; }
                if (lvUsers.SelectedItems[0].Tag is USER u)
                {
                    u.Username = txtUser.Text;
                    u.Password = txtPass.Text;
                    if (cmbRole.SelectedItem != null) u.Rol = cmbRole.SelectedItem.ToString();
                    if (DatabaseHelper.UpdateUtilizator(u))
                    {
                        RefreshUsers();
                        MessageBox.Show("Utilizator actualizat cu succes!");
                    }
                }
            };

            // ── sp_DeleteUtilizator ──
            btnDeleteUser.Click += (s, e) =>
            {
                if (lvUsers.SelectedItems.Count == 0) { MessageBox.Show("Selectați un utilizator!"); return; }
                if (lvUsers.SelectedItems[0].Tag is USER u)
                {
                    if (MessageBox.Show($"Ștergi utilizatorul {u.Username}?", "Confirmare",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (DatabaseHelper.DeleteUtilizator(u.ID))
                        {
                            RefreshUsers();
                            txtUser.Clear(); txtPass.Clear(); cmbRole.SelectedIndex = -1;
                        }
                    }
                }
            };

            pnlContent.Controls.AddRange(new Control[] {
                pnlTitleBar, lvUsers,
                lblUser, txtUser, lblPass, txtPass, lblRole, cmbRole,
                btnAddUser, btnUpdateUser, btnDeleteUser });
        }

        // ════════════════════════════════════════════════
        //  REPORTS
        // ════════════════════════════════════════════════
        private void ShowReports()
        {
            pnlContent.Controls.Clear();

            Panel pnlTitleBar = MakeTitleBar("General Reports & Dashboard");

            // ── sp_GetRaportDepozit ──
            var (totalUnice, totalBucati, valoare) = DatabaseHelper.GetRaportDepozit();

            // ── sp_GetProduseStocCritic ──
            var stocCritic = DatabaseHelper.GetProduseStocCritic();

            Panel pnlMain = new Panel();
            pnlMain.BackColor = System.Drawing.Color.FromArgb(55, 68, 82);
            pnlMain.Size = new System.Drawing.Size(450, 450);
            int cx = (pnlContent.Width > 0 ? pnlContent.Width : 1000) / 2 - 225;
            int cy = 60 + ((pnlContent.Height > 0 ? pnlContent.Height : 650) - 60) / 2 - 225;
            pnlMain.Location = new System.Drawing.Point(cx, cy);

            Label lblStats = new Label();
            lblStats.Text =
                $" STATISTICI GENERALE DEPOZIT\n\n" +
                $"• Total produse unice: {totalUnice}\n" +
                $"• Total bucăți fizice în stoc: {totalBucati} buc.\n" +
                $"• Valoarea totală a mărfii: {valoare:N2} MDL";
            lblStats.ForeColor = System.Drawing.Color.White;
            lblStats.Font = new System.Drawing.Font("Arial", 14);
            lblStats.Location = new System.Drawing.Point(20, 20);
            lblStats.AutoSize = true;

            Label lblCriticalTitle = new Label();
            lblCriticalTitle.Text = "!!! ATENȚIE STOC CRITIC (Sub 5 bucăți)";
            lblCriticalTitle.ForeColor = System.Drawing.Color.Orange;
            lblCriticalTitle.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            lblCriticalTitle.Location = new System.Drawing.Point(20, 150);
            lblCriticalTitle.AutoSize = true;

            ListView lvCritical = new ListView();
            lvCritical.Location = new System.Drawing.Point(20, 190);
            lvCritical.Size = new System.Drawing.Size(405, 240);
            lvCritical.View = View.Details;
            lvCritical.FullRowSelect = true;
            lvCritical.GridLines = true;
            lvCritical.BackColor = System.Drawing.Color.FromArgb(33, 44, 57);
            lvCritical.ForeColor = System.Drawing.Color.White;
            lvCritical.Columns.Add("Cod Produs", 100);
            lvCritical.Columns.Add("Nume Produs", 200);
            lvCritical.Columns.Add("Stoc Rămas", 100);

            if (stocCritic.Count == 0)
                lvCritical.Items.Add(new ListViewItem("Toate produsele au stoc suficient!")
                { ForeColor = System.Drawing.Color.LightGreen });
            else
                foreach (var p in stocCritic)
                {
                    var item = new ListViewItem(p.Cod);
                    item.SubItems.Add(p.Nume);
                    item.SubItems.Add(p.Cantitate.ToString());
                    item.ForeColor = System.Drawing.Color.OrangeRed;
                    lvCritical.Items.Add(item);
                }

            pnlMain.Controls.AddRange(new Control[] { lblStats, lblCriticalTitle, lvCritical });
            pnlContent.Controls.AddRange(new Control[] { pnlTitleBar, pnlMain });
        }

        // ── Helpers UI ──
        private Panel MakeTitleBar(string title)
        {
            var pnl = new Panel();
            pnl.BackColor = System.Drawing.Color.FromArgb(55, 68, 82);
            pnl.Dock = DockStyle.Top;
            pnl.Height = 60;
            var lbl = new Label();
            lbl.Text = title;
            lbl.ForeColor = System.Drawing.Color.White;
            lbl.Font = new System.Drawing.Font("Arial", 18, System.Drawing.FontStyle.Bold);
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            pnl.Controls.Add(lbl);
            return pnl;
        }

        private Label CreateLabel(string text, int y, int x)
        {
            var lbl = new Label();
            lbl.Text = text;
            lbl.ForeColor = System.Drawing.Color.White;
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.AutoSize = true;
            return lbl;
        }

        private TextBox CreateTextBox(int y, int x, string name)
        {
            var txt = new TextBox();
            txt.Name = name;
            txt.Location = new System.Drawing.Point(x, y + 18);
            txt.Width = 200;
            return txt;
        }

        private Button CreateButton(string text, int y, int x)
        {
            var btn = new Button();
            btn.Text = text;
            btn.Location = new System.Drawing.Point(x, y + 290);
            btn.Size = new System.Drawing.Size(140, 35);
            btn.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);
            btn.ForeColor = System.Drawing.Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            return btn;
        }

        private Produs BuildProdus(TextBox cod, TextBox nume, TextBox cat,
            TextBox pret, TextBox qty, TextBox loc, TextBox desc)
        {
            return new Produs
            {
                Cod = cod.Text.Trim(),
                Nume = nume.Text.Trim(),
                Categorie = cat.Text.Trim(),
                Pret = decimal.TryParse(pret.Text, out var p) ? p : 0,
                Cantitate = int.TryParse(qty.Text, out var q) ? q : 0,
                Locatie = loc.Text.Trim(),
                Descriere = desc.Text.Trim()
            };
        }

        private void ClearProductFields(params TextBox[] fields)
        {
            foreach (var f in fields) { f.Clear(); f.ReadOnly = false; }
        }
    }
}
