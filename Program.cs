using System.ComponentModel.Design;
using System.Globalization;

namespace Trabajo_N_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            MostrarBannerInstitucional();

            string nombrePyme = SolicitarTextoVacio("Ingrese nombre de su PYME: ");
            string comuna = SolicitarTextoVacio("Ingrese la comuna donde se ubica la PYME: ");

            // Capturas robustas con validación
            int cantPaneles = SolicitarEnteroValido("Cantidad de paneles requeridos (1 a 200): ", 1, 200);
            double tarifaPanel = SolicitarDecimalValido("Tarifa unitaria base por panel en USD (50.0 a 1500.0): ", 50.0, 1500.0);
            double costoInversor = SolicitarDecimalValido("Costo de equipos inversores y montaje en USD (100.0 a 10000.0): ", 100.0, 10000.0);
            double dctoFomento = SolicitarDecimalValido("Porcentaje de descuento por fomento estatal (0 a 30%): ", 0.0, 30.0);
            double presupuestoMax = SolicitarDecimalValido("Presupuesto mensual límite de la PYME en USD (500.0 a 100000.0): ", 500.0, 100000.0);

            // Lógica de cálculo modular
              double subtotalNeto = CalcularSubtotal(cantPaneles, tarifaPanel, costoInversor);
              double montoDescuento = CalcularDescuentoFomento(subtotalNeto, dctoFomento);
              double netoConDescuento = subtotalNeto - montoDescuento;
              double montoIva = CalcularIvaChileno(netoConDescuento);
              double totalCotizacion = netoConDescuento + montoIva;

            // Evaluar la viabilidad económica
            bool esViable = EvaluarViabilidadEconomica(totalCotizacion, presupuestoMax);

            // Presentación visual del presupuesto
            MostrarDetalleCotizacion(nombrePyme, comuna, cantPaneles, subtotalNeto, montoDescuento, netoConDescuento, montoIva, totalCotizacion, esViable, presupuestoMax);

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();


        }

        static void MostrarBannerInstitucional()
        {
            Console.WriteLine("===========================");
            Console.WriteLine(" Cotizador de energia solar");
            Console.WriteLine("===========================");
        }

        static string SolicitarTextoVacio(string prompt)
        {
            string entrada = "";
            while (entrada == "")
            {
                Console.Write(prompt);
                entrada = Console.ReadLine();
                if (entrada == "")
                {
                    Console.WriteLine("El campo no puede estar vacio.");
                }
            }
            return entrada;
        }

        static int SolicitarEnteroValido(string prompt, int min, int max)
        {
            int paneles;
            bool panelesValidos;

            do
            {
                Console.Write(prompt);

                panelesValidos = int.TryParse(Console.ReadLine(), out paneles);
                if (!panelesValidos || paneles < min || paneles > max)
                {
                    Console.WriteLine("Ingrese un numero entero entre 1 y 200");
                }
            } while (!panelesValidos || paneles < min || paneles > max);

            return paneles;
        }
        static double SolicitarDecimalValido(string prompt, double min, double max)
        {
            double tarifa;
            bool tarifaValida;

            do
            {
                Console.Write(prompt);
                tarifaValida = double.TryParse(Console.ReadLine(), out tarifa);

                if (!tarifaValida || tarifa < min || tarifa > max)
                {
                    Console.WriteLine($"Ingrese un numero decimal entre {min} y {max}");
                }

            } while (!tarifaValida || tarifa < min || tarifa > max);

            return tarifa;
        }
        static double CalcularSubtotal(int paneles, double precioPanel, double inversores) 
        {
            double precioPaneles = paneles * precioPanel ;
            double subTotal = precioPanel + inversores ;
            return subTotal ;
        }
        static double CalcularDescuentoFomento(double bruto, double dcto) 
        {
            double descuentoFomento = (dcto * bruto)/100 ;
            return descuentoFomento ;
        }
        static double CalcularIvaChileno(double netoAfecto) 
        {
            double ivaChileno = (netoAfecto * 19)/100 ;
            return ivaChileno ;
        }

        static bool EvaluarViabilidadEconomica(double total, double presupuestoPyme)
        {
            double presupuestoPyme10 = presupuestoPyme+((presupuestoPyme*10)/100) ;
            if (total <= presupuestoPyme || total <= presupuestoPyme10)
            {
                return true ;
            }
            else 
            {
                return false ;
            }
        }
        static void MostrarDetalleCotizacion(string nPyme, string lugar, int cantidadPaneles, double subNeto, double montDescuento, double netConDescuento, double montIva, double totalCoti, bool viable, double presuMax) 
        {
            string Viable;
            if (viable = true)
            {
                Viable = "Valido";
            }
            else 
            {
                Viable = "No Valido";
            }
            Console.WriteLine("========================================================");
            Console.WriteLine("             COTIZACIÓN VALIDADA EXITOSAMENTE");
            Console.WriteLine("========================================================");
            Console.WriteLine($"Cliente:               {nPyme}");
            Console.WriteLine($"Comuna:                {lugar}");
            Console.WriteLine($"Cuantos paneles:       {cantidadPaneles:F2}");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine($"Subtotal Neto:        {subNeto:F2} USD");
            Console.WriteLine($"Desc.Fomento:         {montDescuento:F2} USD");
            Console.WriteLine($"Neto Final:           {netConDescuento:F2} USD");
            Console.WriteLine($"IVA (19%):            {montIva:F2} USD");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine($"Total Cotizacion:     {totalCoti:F2} USD");
            Console.WriteLine($"Presupuesto Maximo:   {presuMax:F2} USD");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine($"Viabilidad:           {Viable}");
            Console.WriteLine("========================================================");
        }
    }
}