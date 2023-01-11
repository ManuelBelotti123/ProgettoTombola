using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgettoTombola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //dichiarazione
            Random random = new Random();
            int n = 0, riga = 0, colonna = 0;
            bool c = false, e = false;
            string n1 = "";
            string[,] cart = new string[3, 9], cart1 = new string[3, 9], tab = new string[9, 10];
            //stampa del tabellone
            riga = 2;
            colonna = 40;
            tab = Tabellone(tab, riga, colonna);
            Console.SetCursorPosition(colonna, riga - 1);
            Console.WriteLine("Tabellone");
            TabelloneStampa(tab, riga, colonna);
            //generazione della prima cartella
            riga = 15;
            colonna = 5;
            cart = GenerazioneCartella(cart, random);
            Console.SetCursorPosition(colonna, riga - 1);    
            Console.WriteLine("Cartella 1");
            CartellaStampa(cart, riga, colonna);
            //generazione della seconda cartella
            riga = 15;
            colonna = 70;
            cart1 = GenerazioneCartella(cart1, random);
            Console.SetCursorPosition(colonna, riga - 1);
            Console.WriteLine("Cartella 2");
            CartellaStampa(cart1, riga, colonna);
            //estrazione del numero casuale
            while (true)
            {
                Thread.Sleep(1000);
                n = random.Next(1, 91);
                if (n < 10)
                {
                    n1 = " " + n.ToString();
                }
                else
                {
                    n1 = n.ToString();
                }
                Console.SetCursorPosition(49, 12);
                Console.WriteLine("Numero estratto: " + n);
                //aggiornamento del tabellone
                riga = 2;
                colonna = 40;
                TabelloneAggiornamento(tab, n, riga, colonna);
                //verifica della corrisponenza nelle cartelle
                riga = 15;
                colonna = 5;
                CartellaVerificaCorrispondenza(cart, n1, riga, colonna);
                riga = 15;
                colonna = 70;
                CartellaVerificaCorrispondenza(cart1, n1, riga, colonna);
                //verifica cinquine
                riga = 15;
                colonna = 5;
                e = false;
                e = VerificaCinquina(cart, riga, colonna, e);
                if (e)
                {
                    Console.SetCursorPosition(colonna, riga + 5);
                    Console.WriteLine("Cartella 1: Cinquina!");
                }
                riga = 15;
                colonna = 70;
                e = false;
                e = VerificaCinquina(cart1, riga, colonna, e);
                if (e)
                {
                    Console.SetCursorPosition(colonna, riga + 5);
                    Console.WriteLine("Cartella 2: Cinquina!");
                }
                //verifica tombola
                riga = 15;
                colonna = 5;
                c = false;
                c = VerificaTombola(cart, riga, colonna, c);
                if (c)
                {
                    Console.SetCursorPosition(colonna, riga + 5);
                    Console.WriteLine("Cartella 1: Tombola! ");
                }
                riga = 15;
                colonna = 70;
                c = false;
                c = VerificaTombola(cart1, riga, colonna, c);
                if (c)
                {
                    Console.SetCursorPosition(colonna, riga + 5);
                    Console.WriteLine("Cartella 2: Tombola! ");
                    break;
                }
            }
        }

        static string[,] GenerazioneCartella(string[,] cartella, Random random)
        {
            //dichiarazione
            cartella = new string[3, 9];
            int n = 1, m = 11;
            //generazione della cartella
            string numerogen = "0";
            for (int i = 0; i < 3; i++)
            {
                n = 1;
                m = 11;
                numerogen = "0";
                for (int j = 0; j < 9; j++)
                {
                    numerogen = random.Next(n, m).ToString();
                    //correzione dell'allineamento dei numeri in colonna
                    if (int.Parse(numerogen) < 10)
                    {
                        numerogen = " " + numerogen.ToString();
                    }
                    //generazione di numeri non ripetuti nella cartella
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 9; l++)
                        {
                            while (cartella[k, l] == numerogen)
                            {
                                numerogen = random.Next(n, m).ToString();
                            }
                        }
                    }
                    cartella[i, j] = numerogen;
                    n = n + 10;
                    m = m + 10;
                }
            }
            //ciclo di inserimento delle caselle vuote
            int randi = 0;
            for (int i = 0; i < 3; i++)
            {
                randi = 0;
                for (int j = 0; j < 4; j++)
                {
                    do
                    {
                        randi = random.Next(0, 9);
                    }
                    while (cartella[i, randi] == "--");
                    cartella[i, randi] = "--";
                }
            }
            return cartella;
        }

        static void CartellaStampa(string[,] cartella, int riga, int colonna)
        {
            //stampa della cartella
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(colonna, riga);
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(cartella[i, j] + "   ");
                }
                riga++;
            }
        }

        static string[,] Tabellone(string[,] tabellone, int riga, int colonna)
        {
            //matrice tabellone
            tabellone = new string[9, 10];
            int n = 1;
            //ciclo per la generazione del tabellone
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (n < 10)
                    {
                        tabellone[i, j] = " " + n.ToString();
                    }
                    else
                    {
                        tabellone[i, j] = n.ToString();
                    }
                    n++;
                }
            }
            return tabellone;
        }

        static void TabelloneStampa(string[,] tabellone, int riga, int colonna)
        {
            //stampa del tabellone
            for (int i = 0; i < 9; i++)
            {
                Console.SetCursorPosition(colonna, riga);
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(tabellone[i, j] + "  ");
                }
                riga++;
            }
        }

        static void TabelloneAggiornamento(string[,] tabellone, int numero, int riga, int colonna)
        {
            //stampa del tabellone
            for (int i = 0; i < 9; i++)
            {
                Console.SetCursorPosition(colonna, riga);
                for (int j = 0; j < 10; j++)
                {
                    if (tabellone[i, j] == numero.ToString())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        tabellone[i, j] = "--";
                    }
                    else if (tabellone[i, j] == " " + numero.ToString())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        tabellone[i, j] = "--";
                    }
                    Console.Write(tabellone[i, j] + "  ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                riga++;
            }
        }

        static void CartellaVerificaCorrispondenza(string[,] cartella, string numero, int riga, int colonna)
        {
            //stampa della cartella con corrispondenza
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(colonna, riga);
                for (int j = 0; j < 9; j++)
                {
                    if (cartella[i, j] == numero)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Thread.Sleep(1000);
                        cartella[i, j] = "^^";
                    }
                    Console.Write(cartella[i, j] + "   ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                riga++;
            }
        }

        static bool VerificaCinquina(string[,] cartella, int riga, int colonna, bool e)
        {
            //dichiarazione
            int contatore = 0;
            //ciclo
            riga = 25;
            for (int i = 0; i < 3; i++)
            {
                contatore = 0;
                for (int j = 0; j < 9; j++)
                {
                    if (cartella[i, j] == "^^")
                    {
                        contatore++;
                    }
                }
                if (contatore == 5)
                {
                    e = true;
                }
            }
            return e;
        }

        static bool VerificaTombola(string[,] cartella, int riga, int colonna, bool c)
        {
            //dichiarazione
            int contatore = 0;
            //ciclo
            riga = 25;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (cartella[i, j] == "^^")
                    {
                        contatore++;
                    }
                }
            }
            if (contatore == 15)
            {
                c = true;
            }
            return c;
        }
    }
}
