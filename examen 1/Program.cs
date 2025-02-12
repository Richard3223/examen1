using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        Menu();
    }

    /// <summary>
    /// Muestra el menú principal del sistema.
    /// </summary>
    static void Menu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Menú ---");
            Console.WriteLine("1. Registrar jornada de trabajo");
            Console.WriteLine("2. Calcular pago de un empleado");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                RegistrarJornada();
            }
            else if (opcion == "2")
            {
                CalcularPago();
            }
            else if (opcion == "3")
            {
                Console.WriteLine("Saliendo del sistema.");
                break;
            }
            else
            {
                Console.WriteLine("Opción inválida, intente de nuevo.");
            }
        }
    }

    /// <summary>
    /// Registra la jornada de trabajo de un empleado solicitando su nombre, actividad y horario en formato de 12 horas (AM/PM).
    /// </summary>
    static void RegistrarJornada()
    {
        Console.Write("Ingrese el nombre del empleado: ");
        string nombre = Console.ReadLine();

        Console.Write("Ingrese la actividad realizada: ");
        string actividad = Console.ReadLine();

        DateTime horaEntrada, horaSalida;

        if (!LeerHora("Ingrese la hora de entrada (hh:mm AM/PM): ", out horaEntrada) ||
            !LeerHora("Ingrese la hora de salida (hh:mm AM/PM): ", out horaSalida))
        {
            Console.WriteLine("Error en el formato de hora.");
            return;
        }

        if (horaSalida < horaEntrada)
        {
            Console.WriteLine("Error: La hora de salida no puede ser antes de la hora de entrada.");
            return;
        }

        double horasTrabajadas = (horaSalida - horaEntrada).TotalHours;
        Console.WriteLine($"Jornada registrada para {nombre} - Actividad: {actividad} - Horas trabajadas: {horasTrabajadas:F2}");
    }

    /// <summary>
    /// Calcula y muestra el pago de un empleado basado en horas trabajadas, tarifa por hora, bonificación y penalización.
    /// </summary>
    static void CalcularPago()
    {
        double horasTrabajadas = LeerDouble("Ingrese las horas trabajadas: ");
        double tarifaHora = LeerDouble("Ingrese la tarifa por hora: ");
        double bonificacion = LeerDouble("Ingrese la bonificación (si aplica): ");
        double penalizacion = LeerDouble("Ingrese la penalización (si aplica): ");

        double salarioBruto = horasTrabajadas * tarifaHora;
        double salarioNeto = salarioBruto + bonificacion - penalizacion;
        salarioNeto = salarioNeto < 0 ? 0 : salarioNeto;  // Evitar valores negativos

        Console.WriteLine($"Salario Bruto: {salarioBruto:F2}, Salario Neto: {salarioNeto:F2}");
    }

    /// <summary>
    /// Solicita una hora en formato de 12 horas (AM/PM) y la convierte en DateTime.
    /// </summary>
    /// <param name="mensaje">Mensaje para el usuario.</param>
    /// <param name="hora">Variable de salida con la hora ingresada.</param>
    /// <returns>True si la hora es válida, False si hay error.</returns>
    static bool LeerHora(string mensaje, out DateTime hora)
    {
        Console.Write(mensaje);
        string entrada = Console.ReadLine();
        return DateTime.TryParseExact(entrada, "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out hora);
    }

    /// <summary>
    /// Solicita y valida un número decimal ingresado por el usuario.
    /// </summary>
    /// <param name="mensaje">Mensaje para el usuario.</param>
    /// <returns>El valor numérico ingresado.</returns>
    static double LeerDouble(string mensaje)
    {
        double valor;
        while (true)
        {
            Console.Write(mensaje);
            if (double.TryParse(Console.ReadLine(), out valor))
            {
                return valor;
            }
            Console.WriteLine("Entrada inválida. Ingrese un número válido.");
        }
    }
}
