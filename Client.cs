using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MoldCom
{

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
        // ── Componente principale ──
        private TabControl tabMain;
        private TabPage tabMagazin, tabCont;

        // ── Tab Magazin ──
        private ListView lvProducts;
        private TextBox txtSearch, txtDetails;
        private NumericUpDown nudQuantity;
        private Button btnSearch, btnAddToCart, btnViewCart, btnPlaceOrder;
        private Label lblWelcome;
        private List<CartItem> shoppingCart = new List<CartItem>();

        // ── Tab Cont – Date personale ──
        private TextBox txtNume, txtPrenume, txtTelefon, txtEmail, txtAdresa;
        private Button btnSalveazaDate;
        private Label lblStatusDate;

        // ── Tab Cont – Schimbare parolă ──
        private TextBox txtParolaVeche, txtParolaNouA, txtParolaConfirm;
        private Button btnSchimbaParola;
        private Label lblStatusParola;

        // ── Utilizatorul curent ──
        private USER currentUser;

        // ─────────────────────────────────────────────────────────
        //  Constructor
        // ─────────────────────────────────────────────────────────
        public Client(USER user = null)
        {
            currentUser = user;
            InitializeComponent();
            LoadProducts();

            if (currentUser != null)
            {
                lblWelcome.Text = $"Bun venit, {currentUser.Username}!";
                IncarcaDateCont();
            }
        }

        // ─────────────────────────────────────────────────────────
        //  InitializeComponent
        // ─────────────────────────────────────────────────────────
        private void InitializeComponent()
        {
            this.Text = "MoldCom Online Store  [V2]";
            this.Size = new Size(820, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(44, 55, 68);
            this.MinimumSize = new Size(820, 620);

            // ── Header ──────────────────────────────────────────
            Label lblTitle = new Label
            {
                Text = "MoldCom Online Store",
                ForeColor = Color.White,
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(15, 15),
                AutoSize = true
            };

            lblWelcome = new Label
            {
                Text = "Bun venit, Client",
                ForeColor = Color.LightGray,
                Location = new Point(530, 20),
                AutoSize = true
            };

            Button btnLogout = new Button
            {
                Text = "Logout",
                Location = new Point(715, 12),
                Size = new Size(80, 30),
                BackColor = Color.FromArgb(160, 60, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) => this.Close();

            // ── TabControl ──────────────────────────────────────
            tabMain = new TabControl
            {
                Location = new Point(10, 55),
                Size = new Size(790, 515),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            tabMagazin = new TabPage
            {
                Text = "  🛒  Magazin  ",
                BackColor = Color.FromArgb(44, 55, 68),
                ForeColor = Color.White
            };

            tabCont = new TabPage
            {
                Text = "  ⚙️  Contul Meu  ",
                BackColor = Color.FromArgb(44, 55, 68),
                ForeColor = Color.White
            };

            tabMain.TabPages.AddRange(new TabPage[] { tabMagazin, tabCont });

            BuildTabMagazin();
            BuildTabCont();

            this.Controls.AddRange(new Control[] { lblTitle, lblWelcome, btnLogout, tabMain });
        }

        // ═════════════════════════════════════════════════════════
        //  TAB 1 – MAGAZIN
        // ═════════════════════════════════════════════════════════
        private void BuildTabMagazin()
        {
            // ── Bara de căutare ──
            Label lblSearch = new Label
            {
                Text = "Caută produs:",
                ForeColor = Color.White,
                Location = new Point(5, 12),
                AutoSize = true
            };

            txtSearch = new TextBox
            {
                Location = new Point(110, 9),
                Width = 220
            };
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnSearch_Click(s, e); };

            btnSearch = new Button
            {
                Text = "Caută",
                Location = new Point(338, 8),
                Size = new Size(70, 26),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(70, 130, 180),
                ForeColor = Color.White
            };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += BtnSearch_Click;

            Button btnReset = new Button
            {
                Text = "Reset",
                Location = new Point(415, 8),
                Size = new Size(60, 26),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(80, 90, 100),
                ForeColor = Color.White
            };
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.Click += (s, e) => { txtSearch.Clear(); LoadProducts(); };

            // ── Lista produse ──
            lvProducts = new ListView
            {
                Location = new Point(5, 45),
                Size = new Size(455, 430),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                BackColor = Color.FromArgb(200, 205, 210),
                ForeColor = Color.Black
            };
            lvProducts.Columns.AddRange(new ColumnHeader[]
            {
                new ColumnHeader { Text = "Produs",     Width = 175 },
                new ColumnHeader { Text = "Categorie",  Width = 130 },
                new ColumnHeader { Text = "Preț",       Width = 145 }
            });
            lvProducts.SelectedIndexChanged += LvProducts_SelectedIndexChanged;

            // ── Panoul detalii + coș ──
            GroupBox grpDetails = new GroupBox
            {
                Text = "Detalii Produs",
                ForeColor = Color.White,
                Location = new Point(468, 45),
                Size = new Size(308, 430)
            };

            txtDetails = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Location = new Point(8, 20),
                Size = new Size(288, 160),
                BackColor = Color.FromArgb(55, 68, 82),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblQty = new Label
            {
                Text = "Cantitate:",
                ForeColor = Color.White,
                Location = new Point(8, 190),
                AutoSize = true
            };

            nudQuantity = new NumericUpDown
            {
                Location = new Point(90, 187),
                Width = 80,
                Minimum = 1,
                Maximum = 100,
                Value = 1,
                BackColor = Color.FromArgb(55, 68, 82),
                ForeColor = Color.White
            };

            Button btnAddToCart = CreazaButon("🛒  Adaugă în coș", 235, Color.FromArgb(60, 140, 80));
            Button btnViewCart = CreazaButon("📋  Vezi coșul", 280, Color.FromArgb(70, 130, 180));
            Button btnPlaceOrder = CreazaButon("✅  Plasează comanda", 325, Color.FromArgb(180, 120, 40));

            btnAddToCart.Click += (s, e) =>
            {
                if (lvProducts.SelectedItems.Count > 0 &&
                    lvProducts.SelectedItems[0].Tag is Produs selectedProduct)
                {
                    int qty = (int)nudQuantity.Value;
                    if (qty > selectedProduct.Cantitate)
                    {
                        MessageBox.Show(
                            $"Stoc insuficient! Disponibil: {selectedProduct.Cantitate} buc.",
                            "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    shoppingCart.Add(new CartItem { Produs = selectedProduct, Quantity = qty });
                    MessageBox.Show($"{qty} × {selectedProduct.Nume} adăugate în coș!", "Succes",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Selectează un produs înainte de a adăuga în coș.", "Atenție",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };

            btnViewCart.Click += (s, e) =>
            {
                if (shoppingCart.Count == 0)
                { MessageBox.Show("Coșul este gol.", "Informație"); return; }
                using (var cf = new CartForm(shoppingCart))
                    cf.ShowDialog();
            };

            btnPlaceOrder.Click += (s, e) =>
            {
                if (shoppingCart.Count == 0)
                { MessageBox.Show("Coșul este gol!", "Atenție"); return; }

                foreach (var item in shoppingCart)
                {
                    DatabaseHelper.ScadeStocProdus(item.Produs.Cod, item.Quantity);
                    item.Produs.Cantitate -= item.Quantity;
                }

                shoppingCart.Clear();
                LoadProducts();
                LvProducts_SelectedIndexChanged(null, null);
                MessageBox.Show("Comanda a fost plasată cu succes!", "Succes",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            grpDetails.Controls.AddRange(new Control[]
            {
                txtDetails, lblQty, nudQuantity,
                btnAddToCart, btnViewCart, btnPlaceOrder
            });

            tabMagazin.Controls.AddRange(new Control[]
            {
                lblSearch, txtSearch, btnSearch, btnReset,
                lvProducts, grpDetails
            });
        }

        // ═════════════════════════════════════════════════════════
        //  TAB 2 – CONTUL MEU
        // ═════════════════════════════════════════════════════════
        private void BuildTabCont()
        {
            // ── Titlu tab ──
            Label lblTabTitle = new Label
            {
                Text = "Setările Contului",
                ForeColor = Color.White,
                Font = new Font("Arial", 13, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            // ════════════════════════════════════
            //  Secțiunea 1 – Date personale
            // ════════════════════════════════════
            GroupBox grpDate = new GroupBox
            {
                Text = "Date Personale",
                ForeColor = Color.LightSkyBlue,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(10, 42),
                Size = new Size(370, 300)
            };

            // Nume
            Label lblNume = CreazaLabel("Nume:", 20, 30);
            txtNume = CreazaTextBox(130, 27, 220);

            // Prenume
            Label lblPrenume = CreazaLabel("Prenume:", 20, 70);
            txtPrenume = CreazaTextBox(130, 67, 220);

            // Telefon
            Label lblTelefon = CreazaLabel("Telefon:", 20, 110);
            txtTelefon = CreazaTextBox(130, 107, 220);

            // Email
            Label lblEmail = CreazaLabel("Email:", 20, 150);
            txtEmail = CreazaTextBox(130, 147, 220);

            // Adresă
            Label lblAdresa = CreazaLabel("Adresă:", 20, 190);
            txtAdresa = CreazaTextBox(130, 187, 220);
            txtAdresa.Width = 220;

            lblStatusDate = new Label
            {
                Text = "",
                ForeColor = Color.LightGreen,
                Location = new Point(10, 230),
                Size = new Size(340, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            btnSalveazaDate = new Button
            {
                Text = "💾  Salvează modificările",
                Location = new Point(10, 255),
                Size = new Size(340, 32),
                BackColor = Color.FromArgb(60, 140, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 9, FontStyle.Bold)
            };
            btnSalveazaDate.FlatAppearance.BorderSize = 0;
            btnSalveazaDate.Click += BtnSalveazaDate_Click;

            grpDate.Controls.AddRange(new Control[]
            {
                lblNume,     txtNume,
                lblPrenume,  txtPrenume,
                lblTelefon,  txtTelefon,
                lblEmail,    txtEmail,
                lblAdresa,   txtAdresa,
                lblStatusDate, btnSalveazaDate
            });

            // ════════════════════════════════════
            //  Secțiunea 2 – Schimbare parolă
            // ════════════════════════════════════
            GroupBox grpParola = new GroupBox
            {
                Text = "Schimbare Parolă",
                ForeColor = Color.LightSkyBlue,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(400, 42),
                Size = new Size(370, 300)
            };

            Label lblParolaVeche = CreazaLabel("Parola actuală:", 15, 30);
            txtParolaVeche = CreazaTextBox(160, 27, 190);
            txtParolaVeche.PasswordChar = '●';

            Label lblParolaNouA = CreazaLabel("Parolă nouă:", 15, 80);
            txtParolaNouA = CreazaTextBox(160, 77, 190);
            txtParolaNouA.PasswordChar = '●';

            // Indicator putere parolă
            Label lblPutere = new Label
            {
                Text = "Putere parolă:",
                ForeColor = Color.LightGray,
                Location = new Point(15, 108),
                AutoSize = true,
                Font = new Font("Arial", 8)
            };

            ProgressBar pbPutere = new ProgressBar
            {
                Location = new Point(110, 109),
                Size = new Size(240, 12),
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                Style = ProgressBarStyle.Continuous
            };

            Label lblPutereText = new Label
            {
                Text = "",
                ForeColor = Color.LightGray,
                Location = new Point(160, 123),
                AutoSize = true,
                Font = new Font("Arial", 7)
            };

            txtParolaNouA.TextChanged += (s, e) =>
            {
                int putere = CalculeazaPutereParola(txtParolaNouA.Text);
                pbPutere.Value = putere;
                if (putere < 30) { lblPutereText.Text = "Slabă"; lblPutereText.ForeColor = Color.OrangeRed; }
                else if (putere < 60) { lblPutereText.Text = "Medie"; lblPutereText.ForeColor = Color.Orange; }
                else if (putere < 85) { lblPutereText.Text = "Bună"; lblPutereText.ForeColor = Color.YellowGreen; }
                else { lblPutereText.Text = "Excelentă"; lblPutereText.ForeColor = Color.LightGreen; }
            };

            Label lblParolaConfirm = CreazaLabel("Confirmă parola:", 15, 145);
            txtParolaConfirm = CreazaTextBox(160, 142, 190);
            txtParolaConfirm.PasswordChar = '●';

            // Hint vizibilitate parolă
            CheckBox chkArata = new CheckBox
            {
                Text = "Arată parolele",
                ForeColor = Color.LightGray,
                Location = new Point(15, 175),
                AutoSize = true,
                Font = new Font("Arial", 8)
            };
            chkArata.CheckedChanged += (s, e) =>
            {
                char c = chkArata.Checked ? '\0' : '●';
                txtParolaVeche.PasswordChar = c;
                txtParolaNouA.PasswordChar = c;
                txtParolaConfirm.PasswordChar = c;
            };

            // Reguli parolă
            Label lblReguli = new Label
            {
                Text = "✔ Min. 8 caractere   ✔ Literă mare   ✔ Cifră",
                ForeColor = Color.FromArgb(150, 180, 210),
                Location = new Point(15, 200),
                AutoSize = true,
                Font = new Font("Arial", 8)
            };

            lblStatusParola = new Label
            {
                Text = "",
                ForeColor = Color.LightGreen,
                Location = new Point(15, 222),
                Size = new Size(340, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            btnSchimbaParola = new Button
            {
                Text = "🔑  Schimbă parola",
                Location = new Point(15, 250),
                Size = new Size(340, 32),
                BackColor = Color.FromArgb(70, 130, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 9, FontStyle.Bold)
            };
            btnSchimbaParola.FlatAppearance.BorderSize = 0;
            btnSchimbaParola.Click += BtnSchimbaParola_Click;

            grpParola.Controls.AddRange(new Control[]
            {
                lblParolaVeche,   txtParolaVeche,
                lblParolaNouA,    txtParolaNouA,
                lblPutere,        pbPutere, lblPutereText,
                lblParolaConfirm, txtParolaConfirm,
                chkArata, lblReguli,
                lblStatusParola, btnSchimbaParola
            });

            // ════════════════════════════════════
            //  Secțiunea 3 – Info cont (read-only)
            // ════════════════════════════════════
            GroupBox grpInfo = new GroupBox
            {
                Text = "Informații Cont",
                ForeColor = Color.LightSkyBlue,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(10, 355),
                Size = new Size(760, 110)
            };

            Label lblUsername = CreazaLabel("Username:", 15, 28);
            Label valUsername = new Label
            {
                Text = currentUser?.Username ?? "—",
                ForeColor = Color.White,
                Location = new Point(120, 28),
                AutoSize = true,
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            Label lblRolInfo = CreazaLabel("Rol:", 15, 58);
            Label valRol = new Label
            {
                Text = currentUser?.Rol ?? "—",
                ForeColor = Color.LightGreen,
                Location = new Point(120, 58),
                AutoSize = true,
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            Label lblIdInfo = CreazaLabel("ID Cont:", 300, 28);
            Label valId = new Label
            {
                Text = currentUser?.ID.ToString() ?? "—",
                ForeColor = Color.White,
                Location = new Point(400, 28),
                AutoSize = true,
                Font = new Font("Arial", 9)
            };

            Label lblClientInfo = CreazaLabel("ID Client:", 300, 58);
            Label valClientId = new Label
            {
                Text = currentUser?.ID.ToString() ?? "—",
                ForeColor = Color.White,
                Location = new Point(400, 58),
                AutoSize = true,
                Font = new Font("Arial", 9)
            };

            // Buton refresh date
            Button btnRefresh = new Button
            {
                Text = "🔄  Reîncarcă datele",
                Location = new Point(580, 35),
                Size = new Size(165, 32),
                BackColor = Color.FromArgb(80, 90, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 8, FontStyle.Bold)
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += (s, e) =>
            {
                IncarcaDateCont();
                lblStatusDate.Text = "Date reîncărcate.";
                lblStatusDate.ForeColor = Color.LightBlue;
            };

            grpInfo.Controls.AddRange(new Control[]
            {
                lblUsername, valUsername,
                lblRolInfo,  valRol,
                lblIdInfo,   valId,
                lblClientInfo, valClientId,
                btnRefresh
            });

            tabCont.Controls.AddRange(new Control[]
            {
                lblTabTitle,
                grpDate,
                grpParola,
                grpInfo
            });
        }

        // ─────────────────────────────────────────────────────────
        //  LOGICA TAB CONT
        // ─────────────────────────────────────────────────────────

        /// <summary>
        /// Încarcă datele clientului din DB în câmpurile formularului.
        /// Apelează sp_GetClientById (sau adaptează la SP-ul tău existent).
        /// </summary>
        private void IncarcaDateCont()
        {
            if (currentUser == null) return;

            try
            {
                // Apelăm DatabaseHelper pentru a obține datele clientului
                var client = DatabaseHelper.GetClientById(currentUser.ID);
                if (client != null)
                {
                    txtNume.Text = client.Nume ?? "";
                    txtPrenume.Text = client.Prenume ?? "";
                    txtTelefon.Text = client.Telefon ?? "";
                    txtEmail.Text = client.Email ?? "";
                    txtAdresa.Text = client.Adresa ?? "";
                }
            }
            catch
            {
                // Dacă metoda nu există încă, lăsăm câmpurile goale
                lblStatusDate.Text = "Nu s-au putut încărca datele.";
                lblStatusDate.ForeColor = Color.OrangeRed;
            }
        }

        private void BtnSalveazaDate_Click(object sender, EventArgs e)
        {
            // ── Validări ──────────────────────────────────────────
            if (string.IsNullOrWhiteSpace(txtNume.Text) ||
                string.IsNullOrWhiteSpace(txtPrenume.Text))
            {
                AfiseazaStatus(lblStatusDate, "Numele și prenumele sunt obligatorii.", false);
                return;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                AfiseazaStatus(lblStatusDate, "Email invalid.", false);
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtTelefon.Text) &&
                txtTelefon.Text.Length < 8)
            {
                AfiseazaStatus(lblStatusDate, "Numărul de telefon este prea scurt.", false);
                return;
            }

            // ── Salvare în DB ──────────────────────────────────────
            bool ok = DatabaseHelper.UpdateDateClient(
                currentUser.ID,
                txtNume.Text.Trim(),
                txtPrenume.Text.Trim(),
                txtTelefon.Text.Trim(),
                txtEmail.Text.Trim(),
                txtAdresa.Text.Trim()
            );

            if (ok)
            {
                AfiseazaStatus(lblStatusDate, "✔ Date salvate cu succes!", true);
                // Actualizăm și label-ul de bun venit
                lblWelcome.Text = $"Bun venit, {txtNume.Text.Trim()} {txtPrenume.Text.Trim()}!";
            }
            else
                AfiseazaStatus(lblStatusDate, "✘ Eroare la salvare. Încearcă din nou.", false);
        }

        private void BtnSchimbaParola_Click(object sender, EventArgs e)
        {
            string veche = txtParolaVeche.Text;
            string noua = txtParolaNouA.Text;
            string confirma = txtParolaConfirm.Text;

            // ── Validare câmpuri goale ──
            if (string.IsNullOrEmpty(veche) || string.IsNullOrEmpty(noua) || string.IsNullOrEmpty(confirma))
            {
                AfiseazaStatus(lblStatusParola, "Completează toate câmpurile.", false);
                return;
            }

            // ── Verificare parolă actuală ──
            USER verificare = DatabaseHelper.Login(currentUser.Username, veche);
            if (verificare == null)
            {
                AfiseazaStatus(lblStatusParola, "✘ Parola actuală este incorectă.", false);
                txtParolaVeche.Clear();
                return;
            }

            // ── Validare parolă nouă ──
            if (noua.Length < 8)
            {
                AfiseazaStatus(lblStatusParola, "✘ Parola trebuie să aibă minim 8 caractere.", false);
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(noua, "[A-Z]"))
            {
                AfiseazaStatus(lblStatusParola, "✘ Parola trebuie să conțină cel puțin o literă mare.", false);
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(noua, "[0-9]"))
            {
                AfiseazaStatus(lblStatusParola, "✘ Parola trebuie să conțină cel puțin o cifră.", false);
                return;
            }

            // ── Confirmare ──
            if (noua != confirma)
            {
                AfiseazaStatus(lblStatusParola, "✘ Parolele noi nu coincid.", false);
                txtParolaConfirm.Clear();
                return;
            }

            // ── Nu permite aceeași parolă ──
            if (veche == noua)
            {
                AfiseazaStatus(lblStatusParola, "✘ Parola nouă trebuie să fie diferită de cea actuală.", false);
                return;
            }

            // ── Salvare în DB ──
            bool ok = DatabaseHelper.SchimbaParola(currentUser.ID, noua);
            if (ok)
            {
                AfiseazaStatus(lblStatusParola, "✔ Parola a fost schimbată cu succes!", true);
                txtParolaVeche.Clear();
                txtParolaNouA.Clear();
                txtParolaConfirm.Clear();
                currentUser.Password = noua;
            }
            else
                AfiseazaStatus(lblStatusParola, "✘ Eroare la schimbarea parolei.", false);
        }

        // ─────────────────────────────────────────────────────────
        //  LOGICA TAB MAGAZIN
        // ─────────────────────────────────────────────────────────
        private void LoadProducts()
        {
            DisplayProducts(DatabaseHelper.GetAllProduse());
        }

        private void DisplayProducts(System.Collections.Generic.IEnumerable<Produs> productsToDisplay)
        {
            lvProducts.Items.Clear();
            foreach (var produs in productsToDisplay)
            {
                ListViewItem item = new ListViewItem(produs.Nume);
                item.SubItems.Add(produs.Categorie);
                item.SubItems.Add($"{produs.Pret:N2} MDL");
                item.Tag = produs;
                lvProducts.Items.Add(item);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string term = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(term)) { LoadProducts(); return; }

            var rezultate = DatabaseHelper.SearchProduse(term);
            DisplayProducts(rezultate);
            if (rezultate.Count == 0)
                MessageBox.Show("Niciun produs găsit!", "Informație",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LvProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvProducts?.SelectedItems.Count > 0 &&
                lvProducts.SelectedItems[0].Tag is Produs p)
            {
                txtDetails.Text =
                    $"Produs: {p.Nume}\r\n" +
                    $"Categorie: {p.Categorie}\r\n" +
                    $"Preț: {p.Pret:N2} MDL\r\n" +
                    $"Stoc disponibil: {p.Cantitate} buc.\r\n\r\n" +
                    $"Descriere:\r\n{p.Descriere}";
            }
            else
                txtDetails?.Clear();
        }

        // ─────────────────────────────────────────────────────────
        //  HELPERS
        // ─────────────────────────────────────────────────────────
        private Button CreazaButon(string text, int y, Color culoare)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(8, y),
                Size = new Size(290, 33),
                BackColor = culoare,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 9, FontStyle.Bold)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private Label CreazaLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                ForeColor = Color.LightGray,
                Location = new Point(x, y),
                AutoSize = true,
                Font = new Font("Arial", 9)
            };
        }

        private TextBox CreazaTextBox(int x, int y, int width)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Width = width,
                BackColor = Color.FromArgb(55, 68, 82),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private void AfiseazaStatus(Label lbl, string mesaj, bool succes)
        {
            lbl.Text = mesaj;
            lbl.ForeColor = succes ? Color.LightGreen : Color.OrangeRed;
        }

        /// <summary>
        /// Calculează un scor 0-100 pentru puterea parolei.
        /// </summary>
        private int CalculeazaPutereParola(string parola)
        {
            if (string.IsNullOrEmpty(parola)) return 0;
            int scor = 0;
            if (parola.Length >= 8) scor += 20;
            if (parola.Length >= 12) scor += 15;
            if (System.Text.RegularExpressions.Regex.IsMatch(parola, "[A-Z]")) scor += 20;
            if (System.Text.RegularExpressions.Regex.IsMatch(parola, "[a-z]")) scor += 10;
            if (System.Text.RegularExpressions.Regex.IsMatch(parola, "[0-9]")) scor += 20;
            if (System.Text.RegularExpressions.Regex.IsMatch(parola, "[^a-zA-Z0-9]")) scor += 15;
            return Math.Min(scor, 100);
        }
    }
}