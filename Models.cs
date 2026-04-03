using System;

namespace MoldCom
{
    // ── Clasa Produs – identică cu V1, compatibilă cu toate formularele ──
    public class Produs
    {
        public string Cod { get; set; }
        public string Nume { get; set; }
        public string Categorie { get; set; }
        public decimal Pret { get; set; }
        public string Descriere { get; set; }
        public int Cantitate { get; set; }
        public string Locatie { get; set; }
    }

    // ── Clasa USER – acum cu ID pentru operații DB ──
    public class USER
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
    }

    // ── CartItem – identic cu V1 ──
    public class CartItem
    {
        public Produs Produs { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Produs.Pret * Quantity;
    }
}
