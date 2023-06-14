using System.Collections.Generic;
using System.Linq.Expressions;

public class Program
{
    //menampilkan Menu
    static void Menu()
    {
        Console.WriteLine("==========================================");
        Console.WriteLine("             MENU GANJIL GENAP           ");
        Console.WriteLine("------------------------------------------");
        Console.WriteLine("1. Cek Ganjil Genap");
        Console.WriteLine("2. Tampilkan Ganjil/Genap (dengan limit)");
        Console.WriteLine("3. Exit");
        Console.WriteLine("------------------------------------------");
    }

    // Memeriksa apakah angka ganjil atau genap
    static string EvenOddCheck(int input)
    {
        if (input % 2 == 0)
        {
            return "genap";
        }
        else
        {
            return "ganjil";
        }
    }
    // Mencetak angka ganjil atau genap dengan limit
    static void PrintEvenOdd(int limit, string choice)
    {
        Console.WriteLine($"Print bilangan 1 - {limit}: ");
        for (int i = 1; i <= limit; i++)
        {
            if ((choice == "ganjil" && i % 2 != 0) || (choice =="genap" && i % 2 == 0))
            {
                Console.Write(i + ", ");
            }
        }
        Console.WriteLine();

    }

    public static void Main()
    {
        // Membuat looping selama exit masih bernilai false maka program akan terus berjalan
        bool exit = false;
        while (!exit)
        {
           //Memanggil fungsi Menu() untuk menampilkan menu pilihan
           Menu();
            Console.Write("Pilihan: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            //Membuat beberapa pilihan kondisi
            switch (choice)
            {
                //jika nilai case 1  pengguna memasukkan sebuah bilangan, kemudian memanggil fungsi EvenOddCheck
                case 1:
                    Console.Write("Masukkan bilangan yang ingin di cek: ");
                    int input = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine($"{input} adalah angka {EvenOddCheck(input)}");
                    break;

                //Jika nilai case 2, maka blok kode di dalam case 2 akan dieksekusi
                case 2:
                    //meminta pengguna memilih antara "ganjil" atau "genap"
                    Console.Write("Pilih (Ganjil/Genap): ");
                    string typeChoice = Console.ReadLine().ToLower();
                    if (typeChoice == "ganjil" || typeChoice == "genap")
                    {
                        //memasukkan sebuah batas(limit)
                        Console.Write("Masukkan limit : ");
                        int limit = Convert.ToInt32(Console.ReadLine());
                        //memanggil fungsi PrintEvenOdd
                        PrintEvenOdd(limit, typeChoice);
                    }
                    //jika input selain dari "ganjil" atau "genap" maka akan muncul
                    else
                    {
                        Console.WriteLine("Input pilihan tidak valid!!!");
                    }
                    break;
                case 3:
                    exit = true;
                    break;
                //jika input selain dari pilihan maka akan muncul
                default:
                    Console.WriteLine("Opsi yang Anda pilih tidak valid. Silakan coba lagi.");
                    break;
            }
        }
    }
    

}