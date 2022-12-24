using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
            int[,] cart1 = new int[3, 9];
            //matrice cartella
            cart1 = GenerazioneCartella(cart1);
            //stampa della cartella
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(0, i);
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(cart1[i, j] + "   ");
                }
            }
        }

        static int[,] GenerazioneCartella(int[,] cartella)
        {
            //dichiarazione
            Random random = new Random();
            cartella = new int[3, 9];
            int[,] supporto = new int[3, 9];
            int n = 1, m = 11, sup = 0;
            //generazione della cartella
            for (int i = 0; i < 3; i++)
            {
                n = 1;
                m = 11;
                for (int j = 0; j < 9; j++)
                {
                    cartella[i, j] = random.Next(n, m);
                    supporto[i, j] = cartella[i, j];
                    n = n + 10;
                    m = m + 10;
                }
            }
            //ciclo di modifica dei numeri ripetuti nella cartella
            
            //ciclo di inserimento delle caselle vuote
            for (int i = 0; i < 3; i++)
            {
                for (int j = random.Next(0, 2); j < 9; j = j + 2)
                {
                    cartella[i, j] = 0;
                }
            }
            return cartella;
        }
    }
}
