# MoldCom — Sistem de Gestionare a Magazinului și Depozitului de Electronice

O aplicație desktop construită cu **C# Windows Forms** care simulează un sistem complet de vânzare cu amănuntul și gestionare a depozitului de electronice. Aplicația dispune de control al accesului bazat pe roluri, permițând utilizatorilor diferiți (Admin, Angajat, Client) să interacționeze cu același inventar de produse în moduri distincte.

---

## ⚙️ Tehnologii Utilizate

| Tehnologie | Detalii |
|---|---|
| Limbaj | C# (.NET Framework) |
| Interfață grafică | Windows Forms (WinForms) |
| Strat de date | Liste statice în memorie (`DataStore`, `ListaUser`) |
| IDE | Visual Studio |
| Arhitectură | Aplicație multi-formular cu acces bazat pe roluri |

> **Nu este utilizată nicio bază de date externă.** Toate datele (produse, utilizatori) sunt stocate în liste statice în memorie, inițializate la pornirea aplicației prin `DataStore.InitializeData()` și `ListaUser.InitializeData()`.

---

## 🚀 Funcționalități

**Autentificare**
- Autentificare cu nume de utilizator și parolă
- Redirecționare în funcție de rol: Admin → Angajat → Client
- Opțiune de afișare/ascundere a parolei

**Panou Admin**
- Gestionare completă a produselor: adăugare, actualizare, ștergere, căutare
- Gestionare completă a utilizatorilor: adăugare, actualizare, ștergere și atribuire de roluri
- Tablou de bord cu rapoarte generale și alerte pentru stoc critic

**Dashboard Angajat**
- Vizualizarea și căutarea inventarului de produse
- Actualizarea cantităților din stoc pentru orice produs
- Avertismente pentru stoc scăzut (cantitate sub 5 bucăți)
- Cronometru de sesiune în timp real și sumar al activității

**Magazin Client**
- Navigarea și căutarea în catalogul de produse
- Vizualizarea detaliilor produsului (preț, stoc, descriere)
- Adăugarea produselor în coșul de cumpărături cu selectarea cantității
- Vizualizarea sumarului coșului cu totaluri
- Plasarea comenzilor (scade automat din stocul depozitului)

---

## 🗂️ Structura Aplicației

```
MoldCom/
│
├── Program.cs              # Punctul de intrare — lansează LoginForm
├── LoginForm.cs            # Autentificare + rutare în funcție de rol
├── AdminPan.cs             # Panou admin cu navigare laterală
├── Employee.cs             # Dashboard depozit pentru angajați
├── Client.cs               # Magazin online pentru clienți
├── CartForm.cs             # Dialog coș de cumpărături
├── DepozitProduse.cs       # Modele de date comune (Produs, DataStore)
└── Lista_utilizatori.cs    # Modelul utilizatorului și lista utilizatorilor
```

**Cum circulă datele:**
- `DataStore.ListaProduse` este sursa unică de adevăr pentru toate datele despre produse. Toate cele trei roluri citesc și scriu în aceeași listă.
- `ListaUser.ListaUSR` conține toate conturile de utilizator și este gestionată de Admin.
- Când un Client plasează o comandă, stocul este scăzut direct din `DataStore.ListaProduse`, astfel încât Angajatul și Adminul văd imediat cantitățile actualizate.

---

## 📸 Previzualizare Interfață

> Înlocuiți fiecare placeholder de mai jos cu o captură de ecran reală după ce aplicația rulează.

---

### 📸 Ecranul de Autentificare

![Ecran Autentificare](images/login.png)

Punctul de intrare al aplicației. Utilizatorii introduc datele de autentificare și sunt direcționați către dashboard-ul corespunzător rolului lor (Admin, Angajat sau Client). Include opțiunea de afișare/ascundere a parolei.

---

### 📸 Panou Admin — Gestionare Produse

![Admin Gestionare Produse](images/admin-produse.png)

Vizualizarea implicită când un Admin se autentifică. Afișează lista completă de produse cu coloane pentru cod, nume, categorie, preț, cantitate și locație în depozit. Formularul din dreapta permite adăugarea, actualizarea, ștergerea și căutarea produselor.

---

### 📸 Panou Admin — Gestionare Utilizatori

![Admin Gestionare Utilizatori](images/admin-utilizatori.png)

Accesibil prin bara laterală. Listează toți utilizatorii înregistrați cu numele de utilizator, parola și rolul. Adminii pot adăuga utilizatori noi, actualiza cei existenți sau îi pot elimina. Rolurile sunt atribuite printr-un meniu dropdown (Admin / Angajat / Client).

---

### 📸 Panou Admin — Rapoarte și Statistici

![Admin Rapoarte](images/admin-rapoarte.png)

O prezentare statistică a depozitului. Afișează numărul total de produse unice, numărul total de unități în stoc și valoarea monetară totală a întregului inventar. Evidențiază și produsele cu stoc critic (sub 5 bucăți).

---

### 📸 Dashboard Angajat

![Dashboard Angajat](images/angajat-dashboard.png)

Interfața de gestionare a depozitului pentru angajați. Include o listă de produse cu funcție de căutare, un cronometru de sesiune în timp real și un formular de actualizare a stocului în panoul din dreapta. Produsele cu stoc scăzut sau epuizat sunt evidențiate în chihlimbar/roșu. Cardul de sesiune urmărește câte actualizări au fost efectuate în sesiunea curentă.

---

### 📸 Magazin Client

![Magazin Client](images/client-magazin.png)

Interfața destinată clienților. Clienții pot naviga prin toate produsele disponibile, pot căuta după nume sau categorie, pot vizualiza informații detaliate despre produs în panoul din dreapta și pot selecta o cantitate pentru a adăuga în coș. Situațiile de lipsă de stoc sunt validate înainte de adăugarea în coș.

---

### 📸 Coș de Cumpărături

![Coș de Cumpărături](images/cos-cumparaturi.png)

Un dialog modal care se deschide când clientul apasă „Vezi Coșul". Afișează toate produsele din coș cu numele, cantitatea și totalul pe linie. Arată totalul general în partea de jos. Este declanșat din fereastra Magazinului Client.

---

## 🔄 Fluxul Aplicației

```
Pornire Aplicație
   └─► LoginForm
         ├─► Admin    ──► AdminPan (Produse / Utilizatori / Rapoarte)
         ├─► Angajat  ──► Dashboard Angajat
         └─► Client   ──► Magazin Client ──► Coș Cumpărături (dialog modal)
```

1. Aplicația pornește la `LoginForm`, care inițializează atât lista de utilizatori, cât și datele despre produse.
2. La autentificarea reușită, se deschide formularul corespunzător și `LoginForm` se ascunde automat.
3. Când utilizatorul se deconectează (închide formularul său), `LoginForm` reapare automat.
4. Toate formularele partajează același `DataStore.ListaProduse` — modificările efectuate într-un formular se reflectă imediat pretutindeni.

---

## 🛠️ Îmbunătățiri Viitoare

- Persistarea datelor într-o bază de date locală (SQLite sau SQL Server) pentru ca modificările să supraviețuiască repornirii aplicației
- Adăugarea criptării parolelor — în prezent parolele sunt stocate și comparate în text simplu
- Implementarea unui istoric al comenzilor pentru clienți
- Adăugarea funcționalității de export în vizualizarea Rapoartelor (PDF sau Excel)
- Îmbunătățirea validării datelor de intrare direct în interfață, nu doar prin `MessageBox`
- Îmbunătățiri de layout pentru rezoluții mai mici de ecran

---

## 📝 Note

- Proiectul nu utilizează **nicio bibliotecă externă sau pachete NuGet** — exclusiv .NET Framework WinForms nativ.
- Datele se resetează la fiecare repornire a aplicației, deoarece totul este stocat în memorie.
- Proiectul este structurat pentru claritate, cu o separare clară între stratul de date (`DepozitProduse.cs`, `Lista_utilizatori.cs`) și stratul de interfață grafică (fișierele individuale ale formularelor).
