# Raport Tehnic — MoldCom Management System

**Versiune:** V2 · SQL Server Express  
**Tehnologie:** C# Windows Forms · .NET Framework  
**Bază de date:** Microsoft SQL Server Express (`BOOT-PC-001\SQLEXPRESS` · `MoldCom2`)  
**Data raportului:** _19 arpilie 2026_  
**Autor:** _Gaina Valentin_   
**Profesor:** _Covali Eugenia_

---

## Cuprins

1. [Introducere](#1-introducere)
2. [Arhitectura Generală a Aplicației](#2-arhitectura-generală-a-aplicației)
   - 2.1 [Structura Fișierelor](#21-structura-fișierelor)
   - 2.2 [Fluxul Principal de Date](#22-fluxul-principal-de-date)
   - 2.3 [Stratul de Date — DatabaseHelper](#23-stratul-de-date--databasehelper)
3. [Modelele de Date — Models.cs](#3-modelele-de-date--modelscs)
4. [Modulul de Autentificare — LoginForm.cs](#4-modulul-de-autentificare--loginformcs)
   - 4.1 [Interfața Vizuală](#41-interfața-vizuală)
   - 4.2 [Logica de Autentificare și Rutare pe Roluri](#42-logica-de-autentificare-și-rutare-pe-roluri)
   - 4.3 [Interacțiunea cu Baza de Date](#43-interacțiunea-cu-baza-de-date)
5. [Panoul Administratorului — AdminPan](#5-panoul-administratorului--adminpan)
   - 5.1 [Tab Produse — CRUD Complet](#51-tab-produse--crud-complet)
   - 5.2 [Tab Utilizatori — Gestionare Conturi](#52-tab-utilizatori--gestionare-conturi)
   - 5.3 [Tab Rapoarte și Statistici](#53-tab-rapoarte-și-statistici)
6. [Dashboard-ul Angajatului — Employee.cs](#6-dashboard-ul-angajatului--employeecs)
   - 6.1 [Bara de Statistici în Timp Real](#61-bara-de-statistici-în-timp-real)
   - 6.2 [Căutarea și Filtrarea Produselor](#62-căutarea-și-filtrarea-produselor)
   - 6.3 [Actualizarea Stocului](#63-actualizarea-stocului)
7. [Modulul Client — Client.cs](#7-modulul-client--clientcs)
   - 7.1 [Magazinul Online (Tab Shop)](#71-magazinul-online-tab-shop)
   - 7.2 [Coșul de Cumpărături și Plasarea Comenzii](#72-coșul-de-cumpărături-și-plasarea-comenzii)
   - 7.3 [Contul Personal (Tab My Account)](#73-contul-personal-tab-my-account)
8. [Stratul de Bază de Date — Proceduri Stocate](#8-stratul-de-bază-de-date--proceduri-stocate)
   - 8.1 [Proceduri pentru Produse](#81-proceduri-pentru-produse)
   - 8.2 [Proceduri pentru Utilizatori](#82-proceduri-pentru-utilizatori)
   - 8.3 [Proceduri pentru Rapoarte și Client](#83-proceduri-pentru-rapoarte-și-client)
9. [Sistemul de Design — Paleta Vizuală](#9-sistemul-de-design--paleta-vizuală)
10. [Fluxul Complet al Aplicației — Diagrama de Navigare](#10-fluxul-complet-al-aplicației--diagrama-de-navigare)
11. [Dovezi de Funcționare — Screenshots și Confirmări DB](#11-dovezi-de-funcționare--screenshots-și-confirmări-db)
    - 11.1 [Autentificare](#111-autentificare)
    - 11.2 [Admin — Gestionare Produse](#112-admin--gestionare-produse)
    - 11.3 [Admin — Gestionare Utilizatori](#113-admin--gestionare-utilizatori)
    - 11.4 [Admin — Rapoarte](#114-admin--rapoarte)
    - 11.5 [Dashboard Angajat — Actualizare Stoc](#115-dashboard-angajat--actualizare-stoc)
    - 11.6 [Client — Magazin și Plasare Comandă](#116-client--magazin-și-plasare-comandă)
    - 11.7 [Client — Contul Personal](#117-client--contul-personal)
12. [Concluzii și Observații](#12-concluzii-și-observații)

---

## 1. Introducere

**MoldCom Management System** este o aplicație desktop de gestiune a unui magazin și depozit de electronice, construită în C# cu Windows Forms și conectată la o bază de date **Microsoft SQL Server Express**. Scopul aplicației este de a gestiona complet fluxul de lucru al unui magazin: de la administrarea inventarului și utilizatorilor, până la procesul de vânzare către client.

Aplicația implementează un sistem de **control al accesului bazat pe roluri (RBAC)**, cu cinci roluri distincte: `Admin`, `Manager`, `Casier`, `Operator` și `Vizitator`. Fiecare rol este direcționat spre o interfață specifică la autentificare, asigurând că utilizatorii accesează doar funcționalitățile care le sunt permise.

Spre deosebire de versiunea anterioară (V1), care stoca datele în liste statice în memorie, **versiunea V2** utilizează o bază de date SQL Server reală cu proceduri stocate (`stored procedures`) pentru toate operațiunile CRUD. Aceasta garantează persistența datelor între sesiuni și o separare clară a logicii de business față de interfața grafică.

Principalele caracteristici ale sistemului:

- Autentificare securizată cu rutare automată pe roluri
- Gestionare completă a produselor (adăugare, editare, ștergere, căutare)
- Gestionare completă a utilizatorilor
- Dashboard pentru angajați cu monitorizare stoc și actualizare cantități
- Magazin online pentru clienți cu coș de cumpărături și scădere automată a stocului din baza de date
- Sistem de rapoarte pentru inventar
- Gestionarea profilului de client (date personale, schimbare parolă)

---

## 2. Arhitectura Generală a Aplicației

### 2.1 Structura Fișierelor

Proiectul este organizat pe principiul **separării responsabilităților**, fiecare fișier având un rol bine definit:

```
MoldCom/
│
├── Program.cs          → Punctul de intrare al aplicației
├── Models.cs           → Clasele de date (Produs, USER, CartItem, ClientDate)
├── DatabaseHelper.cs   → Stratul de acces la baza de date (toate apelurile SQL)
├── LoginForm.cs        → Formularul de autentificare + rutare pe roluri
├── AdminPan.cs         → Panoul administratorului (produse, utilizatori, rapoarte)
├── Employee.cs         → Dashboard-ul angajatului (vizualizare și actualizare stoc)
└── Client.cs           → Magazinul online + contul personal al clientului
```

**`Program.cs`** este punctul de start — apelează `Application.Run(new LoginForm())`, ceea ce înseamnă că întreaga aplicație pornește de la formularul de autentificare. Nu există inițializare de date în cod (spre deosebire de V1); toate datele provin din baza de date.

### 2.2 Fluxul Principal de Date

```
[SQL Server — MoldCom2]
        │
        ▼
[DatabaseHelper.cs]  ←──── singura componentă care vorbește cu DB
        │
        ├──► LoginForm  →  AdminPan (Admin / Manager)
        │                →  Employee (Casier / Operator)
        │                →  Client  (Vizitator)
        │
        └──── Toate operațiunile se reflectă IMEDIAT în DB
```

Fluxul de date este **unidirecțional**: interfața grafică nu cunoaște SQL-ul. Ea apelează metode din `DatabaseHelper`, care execută proceduri stocate și returnează obiecte C# tipizate (`Produs`, `USER` etc.). Această arhitectură face codul ușor de testat și de întreținut.

### 2.3 Stratul de Date — DatabaseHelper

`DatabaseHelper.cs` este o **clasă statică** care centralizează toate interacțiunile cu baza de date. Toată logica de conectare, trimitere de parametri și mapare a rezultatelor se află exclusiv în acest fișier.

**String-ul de conexiune** este definit ca o constantă privată:

```csharp
private const string ConnectionString =
    @"Server=BOOT-PC-001\SQLEXPRESS;Database=MoldCom2;Integrated Security=True;";
```

Conexiunea folosește **Windows Authentication** (`Integrated Security=True`), eliminând necesitatea de a stoca credențiale SQL în cod. Metoda internă `GetConnection()` deschide o conexiune proaspătă la fiecare apel, iar blocurile `using` garantează că resursele sunt eliberate corect chiar și în caz de eroare.

Fiecare metodă publică din `DatabaseHelper` urmează același pattern curat:
1. Deschide o conexiune
2. Creează un `SqlCommand` de tip `StoredProcedure`
3. Adaugă parametrii necesari
4. Execută comanda și mapează rezultatul într-un obiect C#
5. Returnează rezultatul (sau `false`/`null` în caz de eroare)

Erorile sunt capturate cu `try/catch` și afișate utilizatorului prin `MessageBox` cu informații despre metoda care a eșuat.

---

## 3. Modelele de Date — Models.cs

`Models.cs` definește toate clasele de date folosite în aplicație. Acestea sunt **Plain Old CLR Objects (POCO)** — structuri simple fără logică de business, care servesc drept containere de date între baza de date și interfață.

### Clasa `Produs`

Reprezintă un produs din inventar, cu toate câmpurile din tabela corespunzătoare din baza de date:

```csharp
public class Produs
{
    public string Cod { get; set; }        // Cheie primară (ex: "EL001")
    public string Nume { get; set; }       // Denumirea produsului
    public string Categorie { get; set; } // Categoria (ex: "Laptop", "Telefon")
    public decimal Pret { get; set; }     // Prețul unitar în MDL
    public string Descriere { get; set; } // Descriere detaliată
    public int Cantitate { get; set; }    // Stocul disponibil
    public string Locatie { get; set; }   // Locația fizică în depozit
}
```

### Clasa `USER`

Reprezintă un cont de utilizator din sistem:

```csharp
public class USER
{
    public int ID { get; set; }         // ID autogenerat de baza de date
    public string Username { get; set; }
    public string Password { get; set; } // Stocată în text simplu (V2)
    public string Rol { get; set; }     // Admin / Manager / Casier / Operator / Vizitator
}
```

### Clasa `CartItem`

Reprezintă un element din coșul de cumpărături. Conține o referință la obiectul `Produs` și cantitatea selectată. Proprietatea `TotalPrice` este **calculată automat** (nu este stocată):

```csharp
public class CartItem
{
    public Produs Produs { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice => Produs.Pret * Quantity; // calculat dinamic
}
```

### Clasa `ClientDate`

Definită în `Client.cs`, stochează datele de profil ale unui client (separate de contul de autentificare):

```csharp
public class ClientDate
{
    public string Nume { get; set; }
    public string Prenume { get; set; }
    public string Telefon { get; set; }
    public string Email { get; set; }
    public string Adresa { get; set; }
}
```

Această separare permite ca un utilizator cu rolul `Vizitator` să aibă atât credențiale de autentificare (în tabela `Utilizatori`), cât și date personale extinse (în tabela `Clienti`), legate prin `ID`-ul utilizatorului.

---

## 4. Modulul de Autentificare — LoginForm.cs

### 4.1 Interfața Vizuală

`LoginForm` este primul formular afișat la pornirea aplicației. Este un formular de dimensiune fixă (`420 × 550 px`), neridimensionabil, cu un design dark-mode elegant. Elementele principale ale interfeței sunt:

- **Bara de accent albastră** (3 px, sus) — element de branding consistent cu toate celelalte formulare
- **Zona logo** (`MOLDCOM · ELECTRONICS STORE · MANAGEMENT SYSTEM`) pe fond întunecat
- **Card central** cu câmpurile de autentificare, cu propria bara de accent
- **Câmpul USERNAME** și **câmpul PASSWORD** (cu caracter de mascare `●`)
- **Checkbox „Show password"** — comută între `PasswordChar = '●'` și `'\0'` (vizibil)
- **Eticheta de status** (`lblStatus`) — afișează mesaje de eroare în roșu
- **Butonul LOGIN** — albastru, proeminent
- **Butonul EXIT APPLICATION** — colorat discret în roșu-întunecat
- **Versiunea** afișată în cardul de autentificare: `V2 · SQL Server Express`

Apăsarea tastei `Enter` în oricare dintre câmpuri declanșează automat logica de autentificare (prin `btnLogin.PerformClick()`), eliminând necesitatea de a folosi mouse-ul.

La inițializare, constructorul apelează `DatabaseHelper.TestConnection()`. Dacă conexiunea la SQL Server eșuează, utilizatorul este informat printr-un `MessageBox` cu detalii despre serverul țintă, înainte ca formularul să fie afișat.

### 4.2 Logica de Autentificare și Rutare pe Roluri

Metoda `BtnLogin_Click` gestionează întreaga logică de autentificare:

**Pasul 1 — Validare câmpuri:** Dacă username-ul sau parola sunt goale, se afișează un mesaj de avertizare și metoda se oprește.

**Pasul 2 — Apel DB:** Se apelează `DatabaseHelper.Login(username, password)`, care returnează un obiect `USER` dacă credențialele sunt corecte, sau `null` în caz contrar.

**Pasul 3 — Rutare pe roluri:** Pe baza câmpului `user.Rol`, se instanțiază formularul corespunzător:

```csharp
switch (user.Rol)
{
    case "Admin":    nextForm = new AdminPan();     break;
    case "Manager":  nextForm = new AdminPan();     break; // acces identic cu Admin
    case "Casier":   nextForm = new Employee();     break;
    case "Operator": nextForm = new Employee();     break; // acces identic cu Casier
    case "Vizitator": nextForm = new Client(user); break; // primește obiectul user
    default:
        lblStatus.Text = $"⚠  Unknown role: {user.Rol}";
        this.Show(); return;
}
```

**Pasul 4 — Gestionarea ciclului de viață:** `LoginForm` se ascunde (nu se închide) când apare un formular-copil. La închiderea formularului-copil, `LoginForm` reapare automat prin evenimentul `FormClosed`:

```csharp
nextForm.FormClosed += (s, ev) => this.Show();
nextForm.Show();
```

Câmpurile de username și parolă sunt curățate înainte de a afișa formularul următor, din motive de securitate.

### 4.3 Interacțiunea cu Baza de Date

`LoginForm` apelează două metode din `DatabaseHelper`:

| Metodă | Procedură stocată | Scop |
|--------|------------------|------|
| `TestConnection()` | — | Verifică disponibilitatea SQL Server la startup |
| `Login(username, password)` | `sp_Login` | Autentifică utilizatorul și returnează rolul |

Procedura `sp_Login` primește `@Username` și `@Password` ca parametri și returnează primul rând care corespunde credențialelor. Dacă nu există niciun rând, metoda C# returnează `null`, iar interfața afișează mesajul de eroare corespunzător.

---

## 5. Panoul Administratorului — AdminPan

`AdminPan` este formularul cel mai complex din aplicație. Este accesibil utilizatorilor cu rolul `Admin` sau `Manager` și oferă control deplin asupra produselor, utilizatorilor și vizualizarea rapoartelor. Navigarea între secțiuni se face printr-o **bară laterală stângă** cu butoane, fiecare butun deschizând un alt `TabPage` al controlului `TabControl` din zona principală.

### 5.1 Tab Produse — CRUD Complet

Aceasta este vederea implicită la deschiderea panoului de Admin. Conține:

**Lista produselor** — Un `ListView` în modul `View.Details` cu coloanele: `Cod`, `Nume`, `Categorie`, `Pret`, `Cantitate`, `Locatie`. La încărcare, se apelează `DatabaseHelper.GetAllProduse()` (procedura `sp_GetAllProduse`), iar lista este populată cu toate produsele din baza de date.

**Formularul din dreapta** — Un panou cu câmpuri de input pentru fiecare proprietate a unui produs (`Cod`, `Nume`, `Categorie`, `Pret`, `Descriere`, `Cantitate`, `Locatie`). Selectarea unui produs din `ListView` populează automat câmpurile formularului.

**Operațiunile CRUD disponibile:**

- **Adăugare (`sp_AddProdus`):** Completează toate câmpurile și apasă „Add". Validarea câmpurilor obligatorii se face în C# înainte de apelul DB.
- **Actualizare (`sp_UpdateProdus`):** Selectează un produs, modifică câmpurile dorite, apasă „Update". Codul produsului (`Cod`) servește drept cheie de identificare.
- **Ștergere (`sp_DeleteProdus`):** Selectează un produs și apasă „Delete". O confirmare `MessageBox` este prezentată înainte de ștergere pentru a preveni accidentele.
- **Căutare (`sp_SearchProduse`):** Caută produse după nume sau categorie în timp real, filtrând lista vizibilă.

După orice operațiune de scriere (adăugare, actualizare, ștergere), lista este reîncărcată automat din baza de date pentru a reflecta starea curentă.

### 5.2 Tab Utilizatori — Gestionare Conturi

Funcționează similar cu tab-ul de produse, dar gestionează conturile de utilizator. Conține:

**Lista utilizatorilor** — Afișează toți utilizatorii din tabela `Utilizatori` cu coloanele: `ID`, `Username`, `Password`, `Rol`.

**Formularul de editare** — Câmpuri pentru `Username`, `Password` și un `ComboBox` pentru selectarea rolului din lista predefinită: `Admin`, `Manager`, `Casier`, `Operator`, `Vizitator`.

**Operațiunile disponibile:**

- **Adăugare (`sp_AddUtilizator`):** Creează un cont nou cu credențialele și rolul specificat.
- **Actualizare (`sp_UpdateUtilizator`):** Modifică datele unui utilizator existent, identificat prin `ID`.
- **Ștergere (`sp_DeleteUtilizator`):** Șterge un utilizator după `ID`, cu confirmare prealabilă.

> **Notă de securitate:** În versiunea V2, parolele sunt stocate și afișate în text simplu. O îmbunătățire viitoare esențială este implementarea hash-ului de parolă (ex: BCrypt sau SHA-256 cu salt).

### 5.3 Tab Rapoarte și Statistici

Acest tab oferă o privire de ansamblu statistică asupra depozitului, fără posibilitate de editare. Afișează:

**Indicatori principali** — Obținuți printr-un singur apel la `sp_GetRaportDepozit`, care returnează:
- `TotalProduseUnice` — numărul de referințe distincte de produse
- `TotalBucatiStoc` — suma tuturor cantităților din stoc
- `ValoareTotalaDepozit` — valoarea totală a inventarului (sumă a `Pret × Cantitate` pentru toate produsele)

**Produse cu stoc critic** — O listă separată obținută prin `sp_GetProduseStocCritic`, care returnează toate produsele cu `Cantitate < 5`. Acestea sunt evidențiate vizual pentru a atrage atenția asupra necesității de reaprovizionare.

---

## 6. Dashboard-ul Angajatului — Employee.cs

`Employee.cs` este interfața dedicată utilizatorilor cu rolul `Casier` sau `Operator`. Spre deosebire de Admin, angajatul nu poate adăuga sau șterge produse — el poate doar **vizualiza inventarul** și **actualiza cantitățile de stoc**.

### 6.1 Bara de Statistici în Timp Real

Imediat sub antetul aplicației se află o bară de statistici (`pnlStats`) cu patru carduri informative, actualizate la fiecare încărcare a paginii prin metoda `RefreshStats()`:

| Card | Sursă | Descriere |
|------|-------|-----------|
| Total Produse | `GetAllProduse().Count` | Numărul total de produse în baza de date |
| Stoc Scăzut | `Count(p => p.Cantitate > 0 && p.Cantitate < 5)` | Produse cu stoc între 1 și 4 unități |
| Epuizate | `Count(p => p.Cantitate == 0)` | Produse complet fără stoc |
| Actualizări sesiune | Contor local în memorie | Câte actualizări de stoc s-au efectuat în sesiunea curentă |

**Cronometrul de sesiune** este un `System.Windows.Forms.Timer` cu interval de 1 secundă care actualizează eticheta din antet cu durata sesiunii (`hh:mm:ss`) și ora curentă. Se oprește automat la închiderea formularului (`timer.Dispose()`).

### 6.2 Căutarea și Filtrarea Produselor

Lista de produse (`ListView`) este populată la inițializare prin `DatabaseHelper.GetAllProduse()`. Câmpul de căutare permite filtrarea produselor după **nume sau cod**, apelând `DatabaseHelper.SearchProduseEmployee(term)` (procedura `sp_SearchProduseEmployee`).

**Codificarea vizuală a stocului** în lista de produse oferă feedback instant angajatului:
- **Text normal** (alb-albăstrui) — stoc normal (≥ 5 unități)
- **Text chihlimbar** (`C_AccAmber`) — stoc scăzut (1–4 unități)
- **Text roșu** (`C_AccRed`) — produs epuizat (0 unități)

Rândurile alternează între două nuanțe de fundal pentru lizibilitate.

### 6.3 Actualizarea Stocului

Panoul din dreapta conține formularul de actualizare a stocului cu două câmpuri:
- **Codul produsului** (`txtProductCode`) — codul exact al produsului de modificat
- **Cantitate nouă** (`txtNewQuantity`) — numărul întreg nou (≥ 0)

La apăsarea butonului „Update Stock", se apelează `DatabaseHelper.UpdateStocProdus(cod, qty)`, care execută procedura `sp_UpdateStocProdus`. Aceasta **înlocuiește** cantitatea curentă cu cea nouă (nu adaugă, ci setează).

**Validări implementate:**
1. Câmpurile nu pot fi goale
2. Cantitatea trebuie să fie un număr întreg valid (≥ 0)
3. Codul produsului trebuie să existe în baza de date

**Feedback vizual post-actualizare:**
- ✔ Mesaj verde cu codul și noua cantitate confirmate
- ⚠ Avertisment chihlimbar dacă noua cantitate este sub 5
- ⚠ Mesaj roșu „OUT OF STOCK" dacă cantitatea este 0
- Lista se reîncarcă automat pentru a reflecta modificarea

---

## 7. Modulul Client — Client.cs

`Client.cs` este cel mai complex formular ca funcționalitate pentru utilizatorul final. Este accesibil utilizatorilor cu rolul `Vizitator` și implementează un magazin online complet cu două tab-uri principale: **Shop** și **My Account**.

Constructorul primește obiectul `USER` al utilizatorului autentificat, necesar pentru a încărca și salva datele personale din baza de date.

### 7.1 Magazinul Online (Tab Shop)

**Antetul magazinului** afișează mesajul de bun-venit personalizat cu username-ul clientului, un contor al coșului (`lblCartCount`) actualizat în timp real, și un buton de logout.

**Lista produselor** — Un `ListView` populat cu `DatabaseHelper.GetAllProduse()`. Coloanele afișate sunt: `Cod`, `Produs`, `Categorie`, `Pret`, `Stoc`. La click pe un produs, panoul din dreapta se actualizează cu detalii complete despre produs.

**Bara de căutare** funcționează cu `placeholder text` — la focus, textul „Search by name or category…" dispare; la pierderea focus-ului, reapare dacă câmpul este gol. Căutarea apelează `DatabaseHelper.SearchProduse(term)` (procedura `sp_SearchProduse`).

**Panoul de detalii al produsului** (dreapta) afișează:
- Prețul mare, formatat proeminent (`F_PriceBig`, 22pt Bold)
- Un badge de stoc cu culoare semantică: verde (în stoc), chihlimbar (stoc scăzut), roșu (epuizat)
- Descrierea completă a produsului
- Un `NumericUpDown` pentru selectarea cantității dorite
- Butonul „Add to Cart"

**Validarea la adăugare în coș:**
- Verifică că un produs este selectat
- Verifică că cantitatea dorită nu depășește stocul disponibil
- Dacă produsul există deja în coș, **crește cantitatea** în loc să adauge o linie duplicat
- Actualizează contorul coșului din antet

### 7.2 Coșul de Cumpărături și Plasarea Comenzii

Butonul „View Cart" deschide `CartFormEditable` — un dialog modal (`FormBorderStyle.FixedDialog`) care afișează conținutul curent al coșului.

**`CartFormEditable`** afișează:
- Un `ListView` cu coloanele: `Product`, `Unit Price`, `Qty`, `Subtotal`
- Totalul general (`grand total`) calculat dinamic
- Posibilitatea de a modifica cantitatea unui item selectat prin câmpul `NumericUpDown` și butonul „Update"
- Posibilitatea de a elimina un item cu butonul „Remove"

**Plasarea comenzii** (butonul „Place Order" din fereastra principală) declanșează procesul critic al aplicației:

```
Pentru fiecare CartItem din shoppingCart:
    DatabaseHelper.ScadeStocProdus(item.Produs.Cod, item.Quantity)
    → execută sp_ScadeStocProdus(@Cod, @Cantitate)
    → UPDATE în tabela Produse: Cantitate = Cantitate - @Cantitate
```

Această operațiune **modifică direct stocul în baza de date**, astfel că modificarea este vizibilă imediat în `AdminPan` și `Employee` la reîncărcarea datelor. Stocul este scăzut, nu setat — procedura `sp_ScadeStocProdus` face un `UPDATE ... SET Cantitate = Cantitate - @Cantitate`.

După plasarea comenzii cu succes:
- Coșul este golit complet
- Contorul coșului se resetează la 0
- Lista de produse se reîncarcă pentru a reflecta noile cantități
- Un `MessageBox` confirmă comanda

### 7.3 Contul Personal (Tab My Account)

Tab-ul „My Account" oferă clientului posibilitatea de a gestiona datele personale, împărțit în două secțiuni:

**Secțiunea „Personal Information"** — Câmpuri pentru Nume, Prenume, Telefon, Email, Adresă. La inițializare, se apelează `DatabaseHelper.GetClientById(currentUser.ID)` (procedura `sp_GetClientByUserId`) care populează câmpurile cu datele existente. Salvarea apelează `DatabaseHelper.UpdateDateClient(...)` (procedura `sp_UpdateDateClient`).

**Secțiunea „Change Password"** — Trei câmpuri: parola veche, parola nouă, confirmare parolă. **Indicatorul de putere a parolei** este un `ProgressBar` calculat de metoda `CalcStrength(p)` pe baza criteriilor:

| Criteriu | Puncte |
|---------|--------|
| Minim 8 caractere | +20 |
| Minim 12 caractere | +15 |
| Litere mari | +20 |
| Litere mici | +10 |
| Cifre | +20 |
| Caractere speciale | +15 |
| **Total maxim** | **100** |

Bara de putere se actualizează în timp real la fiecare tastă (`TextChanged`). Schimbarea parolei apelează `DatabaseHelper.SchimbaParola(currentUser.ID, parolaNoua)` (procedura `sp_SchimbaParola`).

**Validări implementate:**
- Parola veche trebuie să corespundă cu cea din baza de date (`currentUser.Password`)
- Parola nouă și confirmarea trebuie să fie identice
- Parola nouă nu poate fi goală

---

## 8. Stratul de Bază de Date — Proceduri Stocate

Toate operațiunile de date sunt encapsulate în **proceduri stocate**, asigurând:
- Separarea logicii SQL de codul C#
- Protecție împotriva SQL Injection (parametrizare completă)
- Posibilitatea de a modifica logica SQL fără a recompila aplicația

### 8.1 Proceduri pentru Produse

| Procedură stocată | Parametri | Acțiune |
|------------------|-----------|---------|
| `sp_GetAllProduse` | — | Returnează toate produsele din inventar |
| `sp_SearchProduse` | `@SearchTerm` | Caută produse după Nume sau Categorie (LIKE) |
| `sp_SearchProduseEmployee` | `@SearchTerm` | Căutare pentru angajat (poate include Cod) |
| `sp_AddProdus` | `@Cod, @Nume, @Categorie, @Pret, @Descriere, @Cantitate, @Locatie` | Inserează un produs nou |
| `sp_UpdateProdus` | `@Cod, @Nume, @Categorie, @Pret, @Descriere, @Cantitate, @Locatie` | Actualizează un produs existent după Cod |
| `sp_DeleteProdus` | `@Cod` | Șterge un produs după Cod |
| `sp_UpdateStocProdus` | `@Cod, @CantitateNoua` | Setează cantitatea la o valoare specifică |
| `sp_ScadeStocProdus` | `@Cod, @Cantitate` | Scade cantitatea cu valoarea dată (la plasarea comenzii) |

### 8.2 Proceduri pentru Utilizatori

| Procedură stocată | Parametri | Acțiune |
|------------------|-----------|---------|
| `sp_Login` | `@Username, @Password` | Autentifică și returnează datele utilizatorului |
| `sp_GetAllUtilizatori` | — | Returnează toți utilizatorii |
| `sp_AddUtilizator` | `@Username, @Password, @Rol` | Creează un utilizator nou |
| `sp_UpdateUtilizator` | `@ID, @Username, @Password, @Rol` | Actualizează datele unui utilizator |
| `sp_DeleteUtilizator` | `@ID` | Șterge un utilizator după ID |
| `sp_SchimbaParola` | `@ID, @ParolaNoua` | Schimbă parola unui utilizator |

### 8.3 Proceduri pentru Rapoarte și Client

| Procedură stocată | Parametri | Acțiune |
|------------------|-----------|---------|
| `sp_GetRaportDepozit` | — | Returnează statistici agregate (total unice, total bucăți, valoare totală) |
| `sp_GetProduseStocCritic` | — | Returnează produsele cu `Cantitate < 5` |
| `sp_GetClientByUserId` | `@IdUtilizator` | Returnează profilul de client pentru un utilizator dat |
| `sp_UpdateDateClient` | `@IdUtilizator, @Nume, @Prenume, @Telefon, @Email, @Adresa` | Actualizează profilul de client |

---

## 9. Sistemul de Design — Paleta Vizuală

Toate formularele din aplicație utilizează un **sistem de design unificat** bazat pe o paletă de culori dark-mode. Constantele sunt definite identic în fiecare formular:

| Constantă | Valoare RGB | Utilizare |
|-----------|------------|-----------|
| `C_BgBase` | `(18, 24, 32)` | Fundal antet, zonă logo |
| `C_BgSurface` | `(26, 34, 46)` | Fondal surface (carduri, bare) |
| `C_BgPanel` | `(34, 44, 58)` | Fundal principal al formularului |
| `C_BgCard` | `(42, 54, 70)` | Carduri, rânduri pare în liste |
| `C_BgInput` | `(22, 30, 42)` | Câmpuri de input |
| `C_Border` | `(52, 68, 88)` | Linii separatoare, borduri |
| `C_TextPrim` | `(230, 236, 244)` | Text principal |
| `C_TextSec` | `(140, 160, 180)` | Text secundar, placeholder |
| `C_AccBlue` | `(56, 132, 220)` | Accent principal, butoane principale |
| `C_AccGreen` | `(48, 168, 108)` | Confirmare, succes |
| `C_AccRed` | `(196, 64, 72)` | Erori, avertizări, logout |
| `C_AccAmber` | `(204, 140, 40)` | Stoc scăzut, avertizări moderate |

Fontul standard este **Segoe UI** în dimensiuni de 7pt–22pt, cu variante `Bold` pentru titluri și etichete de accent.

---

## 10. Fluxul Complet al Aplicației — Diagrama de Navigare

```
[Pornire aplicație]
        │
        ▼
[DatabaseHelper.TestConnection()]
        │
        ├── EȘEC → MessageBox de avertizare → LoginForm se afișează oricum
        │
        └── SUCCES → LoginForm
                        │
                        ▼
              [Introducere credențiale]
                        │
                        ▼
              [DatabaseHelper.Login()]
                        │
              ┌─────────┼──────────────────────────┐
              │         │                          │
              ▼         ▼                          ▼
         Admin /    Casier /                   Vizitator
         Manager   Operator
              │         │                          │
              ▼         ▼                          ▼
         [AdminPan]  [Employee]               [Client(user)]
              │         │                          │
              │    [Vizualizare         [Tab Shop]  [Tab My Account]
              │     inventar]               │            │
              │    [Actualizare         [Adaugă    [Editare date
              │     stoc DB]             în coș]    personale DB]
              │         │               [Place     [Schimbare
              ├─[Produse CRUD DB]        Order      parolă DB]
              ├─[Utilizatori CRUD DB]    → scade
              └─[Rapoarte DB]            stoc DB]
                        │
              ←──────── La închidere ─────────────┘
              LoginForm reapare automat (FormClosed event)
```

---

## 11. Dovezi de Funcționare — Screenshots și Confirmări DB

Această secțiune prezintă capturi de ecran ale aplicației în timp real, împreună cu dovezi că operațiunile se reflectă corect în baza de date SQL Server.

---

### 11.1 Autentificare

**Screenshot 1 — Formularul de Login**

<img width="596" height="792" alt="image" src="https://github.com/user-attachments/assets/38d37e6d-3346-43e0-bf8d-47a21c2ebb03" />


**Screenshot 2 — Mesaj de eroare la credențiale greșite**

<img width="595" height="808" alt="image" src="https://github.com/user-attachments/assets/1ce687c5-57bb-4c04-8516-0d898fa7dc75" />


**Screenshot 3 — Autentificare reușită (Admin)**

<img width="1880" height="1114" alt="image" src="https://github.com/user-attachments/assets/8d8498ad-2c84-44b4-a5a2-e34a638312fb" />

---

### 11.2 Admin — Gestionare Produse

**Screenshot 4 — Adăugarea unui produs nou**

<img width="1885" height="1111" alt="image" src="https://github.com/user-attachments/assets/a1c488c7-f3cb-429a-b4f5-6ac42939dbf8" />


**Screenshot 5 — Adăugarea unui utilizator nou**

<img width="1882" height="1087" alt="image" src="https://github.com/user-attachments/assets/427b62b1-c9bf-49ca-9cf6-9c817c94b9c0" />


**Screenshot 6 — Confirmare în baza de date după adăugare**

> _Adaugarea produs._


<img width="1110" height="73" alt="image" src="https://github.com/user-attachments/assets/7dd51356-5230-44c2-8333-274eabb4486d" />



> _Adaugarea utilizator._

<img width="960" height="45" alt="image" src="https://github.com/user-attachments/assets/58773b8f-3c07-4534-8760-b808e57c1c77" />



**Screenshot 7 — Actualizarea unui produs**

<img width="1872" height="941" alt="image" src="https://github.com/user-attachments/assets/5b6706c6-7b1c-4b9a-9ff0-91c89dd21333" />


<img width="1865" height="1051" alt="image" src="https://github.com/user-attachments/assets/baefd365-5ada-4c52-ae28-29f4717c5a6b" />


**Screenshot 8 — Ștergerea unui produs**

<img width="805" height="128" alt="image" src="https://github.com/user-attachments/assets/a512f334-35a1-4db6-bb98-2435b5040ff4" />



---

### 11.4 Admin — Rapoarte

**Screenshot 12 — Tab-ul de Rapoarte**

<img width="1529" height="1040" alt="image" src="https://github.com/user-attachments/assets/824c0f32-c8a0-4aaf-b0c1-ac5489061c43" />



---
### 11.5 Dashboard Angajat — Actualizare Stoc

**Screenshot 14 — Dashboard-ul Angajatului**

<img width="1611" height="1108" alt="image" src="https://github.com/user-attachments/assets/14c62fe6-6e46-4ad0-abfe-0b138a65e828" />

**Screenshot 15 — Actualizare stoc: ÎNAINTE**

<img width="715" height="159" alt="image" src="https://github.com/user-attachments/assets/2420bf57-59cd-4a27-b8c2-1f1cae33388d" />


**Screenshot 16 — Actualizare stoc: APLICAȚIE**

<img width="1590" height="348" alt="image" src="https://github.com/user-attachments/assets/17a5493e-684d-469e-9c93-b6d3c0428d40" />


**Screenshot 17 — Actualizare stoc: DUPĂ (confirmare DB)**

<img width="1106" height="159" alt="image" src="https://github.com/user-attachments/assets/c0c62d6e-23e6-468b-aeb6-a254b52135da" />


```sql
-- Query de verificare recomandat:
SELECT id_produs, Nume, Cantitate FROM vizitator_MoldCom.Produse WHERE id_produs = '3';
EXEC sp_UpdateStocProdus @id_produs = '3', @CantitateNoua = 37;
SELECT id_produs, Nume, Cantitate FROM vizitator_MoldCom.Produse WHERE Cod = '3';
```

---

### 11.6 Client — Magazin și Plasare Comandă

**Screenshot 18 — Magazinul Online**

<img width="1612" height="1051" alt="image" src="https://github.com/user-attachments/assets/d9fc0d43-2d21-4571-822e-62ed2ab43808" />


**Screenshot 19 — Coșul de cumpărături**

<img width="839" height="698" alt="image" src="https://github.com/user-attachments/assets/f14dfc44-fefb-4353-b0b5-9083a78c0f3e" />


**Screenshot 20 — Stoc ÎNAINTE de plasarea comenzii (DB)**

<img width="1105" height="160" alt="image" src="https://github.com/user-attachments/assets/542d6b9f-a445-4910-a8e1-71b91e34b7e0" />

**Screenshot 21 — Confirmare plasare comandă**

<img width="293" height="47" alt="image" src="https://github.com/user-attachments/assets/de736644-69f9-4031-bba5-2c9ddd15628f" />


**Screenshot 22 — Stoc DUPĂ plasarea comenzii (DB)**

<img width="1112" height="152" alt="image" src="https://github.com/user-attachments/assets/88c0e9b1-56cf-4519-b758-a9a66266e3f1" />


```sql
-- Query de verificare recomandat:
SELECT id_produs, Nume, Cantitate FROM vizitator_MoldCom.Produse WHERE id_produs IN (7);
-- [Plasați comanda în aplicație]
SELECT id_produs, Nume, Cantitate FROM vizitator_MoldCom.Produse WHERE Cod IN (7);
-- Cantitățile ar trebui să fie mai mici cu exact cantitățile comandate
```

---

### 11.7 Client — Contul Personal

**Screenshot 23 — Tab My Account: Date Personale**

<img width="1586" height="1015" alt="image" src="https://github.com/user-attachments/assets/a90d9a97-6167-487d-a3a9-d0b6f4e326af" />


**Screenshot 24 — Salvare date personale: confirmare DB**

<img width="1113" height="98" alt="image" src="https://github.com/user-attachments/assets/f18200de-7f03-4ca4-8edd-72025f830fc0" />


**Screenshot 25 — Schimbare parolă cu indicator de putere**

<img width="605" height="562" alt="image" src="https://github.com/user-attachments/assets/f87215ed-2e9b-42fe-9392-d05394e9360a" />



**Screenshot 26 — Confirmare schimbare parolă în DB**

<img width="970" height="77" alt="image" src="https://github.com/user-attachments/assets/29bb4ad4-9c0d-4408-99b5-30851a6dd350" />


```sql
-- Query de verificare recomandat:
SELECT ID, Username, Password FROM Utilizatori WHERE ID = 7;
```

---

## 12. Concluzii și Observații

**MoldCom Management System V2** reprezintă o aplicație desktop funcțională și coerentă care demonstrează implementarea unui sistem complet de gestiune a unui magazin de electronice. Principalele realizări ale versiunii V2 față de V1 sunt:

**Tranziția la baza de date reală** — Înlocuirea listelor statice în memorie cu SQL Server Express și proceduri stocate este cea mai importantă îmbunătățire. Datele persistă între sesiuni, iar mai mulți utilizatori pot lucra simultan cu același inventar.

**Arhitectura curată** — Separarea completă între stratul de date (`DatabaseHelper`), modelele de date (`Models.cs`) și interfața grafică (formularele) face codul ușor de înțeles, testat și extins.

**Sistemul de roluri granular** — Cele 5 roluri cu accesuri diferențiate reflectă o situație reală din mediul de afaceri, unde nu toți angajații au nevoie de aceleași drepturi.

**Consecvența vizuală** — Folosirea aceluiași sistem de culori și fonturi în toate formularele creează o experiență de utilizare profesională și coerentă.

**Aspecte de îmbunătățit în versiunile viitoare:**

- **Securitatea parolelor** — Implementarea hash-ului de parolă (BCrypt/SHA-256 cu salt) este esențială înainte de orice utilizare în producție. Stocarea în text simplu reprezintă o vulnerabilitate majoră.
- **Istoricul comenzilor** — Momentan, comenzile plasate de clienți nu sunt înregistrate nicăieri; se scade doar stocul. O tabelă `Comenzi` și `DetaliiComanda` ar completa fluxul.
- **Exportul rapoartelor** — Posibilitatea de a exporta rapoartele în PDF sau Excel ar mări utilitatea panoului de Admin.
- **Validare avansată în UI** — Unele validări (ex: format email, format telefon) se pot face direct în interfață cu regex, nu doar prin MessageBox post-submit.
- **Tranzacții SQL** — Plasarea unei comenzi cu mai multe produse ar trebui să fie o tranzacție atomică: dacă scăderea stocului eșuează pentru un produs, întreaga comandă trebuie anulată (`ROLLBACK`).

---

_Raport generat pentru MoldCom Electronics Store — Management System V2_
