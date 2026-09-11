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


    }
}