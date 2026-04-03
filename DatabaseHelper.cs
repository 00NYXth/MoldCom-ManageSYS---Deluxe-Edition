using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MoldCom
{
    /// <summary>
    /// Helper centralizat pentru toate operațiunile cu baza de date.
    /// Schimbă ConnectionString-ul dacă numele bazei de date diferă.
    /// </summary>
    public static class DatabaseHelper
    {
        // ── Modifică doar numele bazei de date dacă e diferit ──
        private const string ConnectionString =
            @"Server=BOOT-PC-001\SQLEXPRESS;Database=MoldCom2;Integrated Security=True;";

        // ─────────────────────────────────────────
        //  Metodă internă – returnează o conexiune deschisă
        // ─────────────────────────────────────────
        private static SqlConnection GetConnection()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }

        // ─────────────────────────────────────────
        //  Testare conexiune (apelată la startup)
        // ─────────────────────────────────────────
        public static bool TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                    return conn.State == ConnectionState.Open;
            }
            catch { return false; }
        }

        // ══════════════════════════════════════════
        //  PRODUSE
        // ══════════════════════════════════════════

        public static List<Produs> GetAllProduse()
        {
            var list = new List<Produs>();
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_GetAllProduse", conn) { CommandType = CommandType.StoredProcedure })
                using (var rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                        list.Add(MapProdus(rdr));
            }
            catch (Exception ex) { ShowError("GetAllProduse", ex); }
            return list;
        }

        public static List<Produs> SearchProduse(string termen)
        {
            var list = new List<Produs>();
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_SearchProduse", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@SearchTerm", termen);
                    using (var rdr = cmd.ExecuteReader())
                        while (rdr.Read())
                            list.Add(MapProdus(rdr));
                }
            }
            catch (Exception ex) { ShowError("SearchProduse", ex); }
            return list;
        }

        public static List<Produs> SearchProduseEmployee(string termen)
        {
            var list = new List<Produs>();
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_SearchProduseEmployee", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@SearchTerm", termen);
                    using (var rdr = cmd.ExecuteReader())
                        while (rdr.Read())
                            list.Add(MapProdus(rdr));
                }
            }
            catch (Exception ex) { ShowError("SearchProduseEmployee", ex); }
            return list;
        }

        public static bool AddProdus(Produs p)
        {
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_AddProdus", conn) { CommandType = CommandType.StoredProcedure })
                {
                    AddProdusParams(cmd, p);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex) { ShowError("AddProdus", ex); return false; }
        }

        public static bool UpdateProdus(Produs p)
        {
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_UpdateProdus", conn) { CommandType = CommandType.StoredProcedure })
                {
                    AddProdusParams(cmd, p);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex) { ShowError("UpdateProdus", ex); return false; }
        }

        public static bool DeleteProdus(string cod)
        {
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_DeleteProdus", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@Cod", cod);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex) { ShowError("DeleteProdus", ex); return false; }
        }

        public static bool UpdateStocProdus(string cod, int cantitateNoua)
        {
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_UpdateStocProdus", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@Cod", cod);
                    cmd.Parameters.AddWithValue("@CantitateNoua", cantitateNoua);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex) { ShowError("UpdateStocProdus", ex); return false; }
        }

        public static bool ScadeStocProdus(string cod, int cantitate)
        {
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_ScadeStocProdus", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@Cod", cod);
                    cmd.Parameters.AddWithValue("@Cantitate", cantitate);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex) { ShowError("ScadeStocProdus", ex); return false; }
        }

        // ══════════════════════════════════════════
        //  UTILIZATORI
        // ══════════════════════════════════════════

        public static USER Login(string username, string password)
        {
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_Login", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    using (var rdr = cmd.ExecuteReader())
                        if (rdr.Read())
                            return new USER
                            {
                                ID = (int)rdr["ID"],
                                Username = rdr["Username"].ToString(),
                                Password = rdr["Password"].ToString(),
                                Rol = rdr["Rol"].ToString()
                            };
                }
            }
            catch (Exception ex) { ShowError("Login", ex); }
            return null;
        }

        public static List<USER> GetAllUtilizatori()
        {
            var list = new List<USER>();
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_GetAllUtilizatori", conn) { CommandType = CommandType.StoredProcedure })
                using (var rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                        list.Add(new USER
                        {
                            ID = (int)rdr["ID"],
                            Username = rdr["Username"].ToString(),
                            Password = rdr["Password"].ToString(),
                            Rol = rdr["Rol"].ToString()
                        });
            }
            catch (Exception ex) { ShowError("GetAllUtilizatori", ex); }
            return list;
        }

        public static bool AddUtilizator(USER u)
        {
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_AddUtilizator", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@Username", u.Username);
                    cmd.Parameters.AddWithValue("@Password", u.Password);
                    cmd.Parameters.AddWithValue("@Rol", u.Rol);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex) { ShowError("AddUtilizator", ex); return false; }
        }

        public static bool UpdateUtilizator(USER u)
        {
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_UpdateUtilizator", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@ID", u.ID);
                    cmd.Parameters.AddWithValue("@Username", u.Username);
                    cmd.Parameters.AddWithValue("@Password", u.Password);
                    cmd.Parameters.AddWithValue("@Rol", u.Rol);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex) { ShowError("UpdateUtilizator", ex); return false; }
        }

        public static bool DeleteUtilizator(int id)
        {
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_DeleteUtilizator", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex) { ShowError("DeleteUtilizator", ex); return false; }
        }

        // ══════════════════════════════════════════
        //  RAPOARTE
        // ══════════════════════════════════════════

        public static (int TotalUnice, int TotalBucati, decimal ValoareTotala) GetRaportDepozit()
        {
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_GetRaportDepozit", conn) { CommandType = CommandType.StoredProcedure })
                using (var rdr = cmd.ExecuteReader())
                    if (rdr.Read())
                        return (
                            (int)rdr["TotalProduseUnice"],
                            (int)rdr["TotalBucatiStoc"],
                            (decimal)rdr["ValoareTotalaDepozit"]
                        );
            }
            catch (Exception ex) { ShowError("GetRaportDepozit", ex); }
            return (0, 0, 0);
        }

        public static List<Produs> GetProduseStocCritic()
        {
            var list = new List<Produs>();
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_GetProduseStocCritic", conn) { CommandType = CommandType.StoredProcedure })
                using (var rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                        list.Add(new Produs
                        {
                            Cod = rdr["Cod"].ToString(),
                            Nume = rdr["Nume"].ToString(),
                            Cantitate = (int)rdr["Cantitate"]
                        });
            }
            catch (Exception ex) { ShowError("GetProduseStocCritic", ex); }
            return list;
        }

        // ─────────────────────────────────────────
        //  Helpers private
        // ─────────────────────────────────────────
        private static Produs MapProdus(SqlDataReader rdr) => new Produs
        {
            Cod = rdr["Cod"].ToString(),
            Nume = rdr["Nume"].ToString(),
            Categorie = rdr["Categorie"].ToString(),
            Pret = (decimal)rdr["Pret"],
            Descriere = rdr["Descriere"] == DBNull.Value ? "" : rdr["Descriere"].ToString(),
            Cantitate = (int)rdr["Cantitate"],
            Locatie = rdr["Locatie"] == DBNull.Value ? "" : rdr["Locatie"].ToString()
        };

        private static void AddProdusParams(SqlCommand cmd, Produs p)
        {
            cmd.Parameters.AddWithValue("@Cod", p.Cod);
            cmd.Parameters.AddWithValue("@Nume", p.Nume);
            cmd.Parameters.AddWithValue("@Categorie", p.Categorie);
            cmd.Parameters.AddWithValue("@Pret", p.Pret);
            cmd.Parameters.AddWithValue("@Descriere", (object)p.Descriere ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Cantitate", p.Cantitate);
            cmd.Parameters.AddWithValue("@Locatie", (object)p.Locatie ?? DBNull.Value);
        }

        private static void ShowError(string method, Exception ex)
        {
            MessageBox.Show($"Eroare DB [{method}]:\n{ex.Message}", "Eroare Bază de Date",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Adaugă aceste 3 metode în clasa DatabaseHelper existentă
        // (în fișierul DatabaseHelper.cs, înainte de ultima acoladă închisă)

        public static ClientDate GetClientById(int idUtilizator)
        {
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_GetClientByUserId", conn)
                { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@IdUtilizator", idUtilizator);
                    using (var rdr = cmd.ExecuteReader())
                        if (rdr.Read())
                            return new ClientDate
                            {
                                Nume = rdr["nume"].ToString(),
                                Prenume = rdr["prenume"].ToString(),
                                Telefon = rdr["telefon"].ToString(),
                                Email = rdr["email"].ToString(),
                                Adresa = rdr["adresa"].ToString()
                            };
                }
            }
            catch (Exception ex) { ShowError("GetClientById", ex); }
            return null;
        }

        public static bool UpdateDateClient(int idUtilizator, string nume, string prenume,
                                            string telefon, string email, string adresa)
        {
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_UpdateDateClient", conn)
                { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@IdUtilizator", idUtilizator);
                    cmd.Parameters.AddWithValue("@Nume", nume);
                    cmd.Parameters.AddWithValue("@Prenume", prenume);
                    cmd.Parameters.AddWithValue("@Telefon", telefon);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Adresa", adresa);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex) { ShowError("UpdateDateClient", ex); return false; }
        }

        public static bool SchimbaParola(int idUtilizator, string parolaNoua)
        {
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand("sp_SchimbaParola", conn)
                { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@ID", idUtilizator);
                    cmd.Parameters.AddWithValue("@ParolaNoua", parolaNoua);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex) { ShowError("SchimbaParola", ex); return false; }
        }
    }
}
