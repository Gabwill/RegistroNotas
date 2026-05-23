using System;

class Estudiante
{
    public string nombre;
    public string idEstudiante;
    public double[] notas = new double[5];
    public double promedio;


    public Estudiante(string nombre, string idEstudiante, double[] notas)
    {
        this.nombre = nombre;
        this.idEstudiante = idEstudiante;
        this.notas = notas;
        this.promedio = 0;
    }


    // METODO PROMEDIO
    public double CalcularPromedio()
    {
        double suma = 0;
        for (int i = 0; i < 5; i++)
        {
            suma += notas[i];
        }
        promedio = suma / 5;
        return promedio;
    }

    // METODO OBTENER ESTADO
    public string ObtenerEstado()
    {
        if (promedio >= 10.5)
        {
            return "APROBADO";
        }
        else
        {
            return "DESAPROBADO";
        }
    }
}

class Program
{
    //LEER ENTEROS
    static int LeerEnteros(int min, int max)
    {
        int ingreso;
        while (!int.TryParse(Console.ReadLine(), out ingreso) || ingreso < min || ingreso > max)
        {
            Console.Write($"Entrada inválida. Elige entre {min} y {max}: ");
        }
        return ingreso;
    }
    //LEER NOTAS
    static double LeerNotas(int min, int max)
    {
        double ingreso;
        while (!double.TryParse(Console.ReadLine(), out ingreso) || ingreso < min || ingreso > max)
        {
            Console.Write($"Entrada inválida. Elige entre {min} y {max}: ");
        }
        return ingreso;
    }
    //IMPRIMIR
    static void Imprimir(Estudiante[] lista, int cantidades)
    {
        Console.WriteLine("-------------------------------------------------------------------");
        Console.WriteLine($"{"NOMBRE",-25} {"ID",-10} {"PROMEDIO",-12} {"ESTADO",-12}");
        Console.WriteLine("-------------------------------------------------------------------");
        for (int i = 0; i < cantidades; i++)
        {
            Console.WriteLine(
                $"{lista[i].nombre,-25} " +
                $"{lista[i].idEstudiante,-10} " +
                $"{lista[i].promedio,-12:0.00} " +
                $"{lista[i].ObtenerEstado(),-12}"
            );
        }
        Console.WriteLine("-------------------------------------------------------------------");
    }
    static void Main(string[] args)
    {
        Estudiante[] estudiantes = new Estudiante[10];

        int total = 0;
        int opcion;

        do
        {
            Console.WriteLine();
            Console.WriteLine("         MENU          \n");
            Console.WriteLine("1. Registrar estudiante");
            Console.WriteLine("2. Calcular promedios");
            Console.WriteLine("3. Buscar estudiante");
            Console.WriteLine("4. Listar estudiantes");
            Console.WriteLine("5. Salir");
            Console.WriteLine("-----------------------");
            Console.Write("Seleccione una opcion: ");

            opcion = LeerEnteros(1, 5);

            switch (opcion)
            {
                case 1:
                    if (total >= 10)
                    {
                        Console.WriteLine("ERROR: No se pueden registrar mas de 10 estudiantes.");
                    }
                    else
                    {
                        Console.Write("Ingrese nombre completo: ");
                        string nom = Console.ReadLine().ToUpper();

                        // Validar que no esté vacío
                        while (nom.Trim().Length == 0)
                        {
                            Console.Write("ERROR: El nombre no puede estar vacío. Ingrese nombre completo: ");
                            nom = Console.ReadLine().ToUpper();
                        }

                        Console.Write("Ingrese ID del estudiante: ");
                        string id = Console.ReadLine().ToUpper();

                        // Validar que no esté vacío
                        while (id.Trim().Length == 0)
                        {
                            Console.Write("ERROR: El ID no puede estar vacío. Ingrese ID del estudiante: ");
                            id = Console.ReadLine().ToUpper();
                        }

                        // VALIDAR ID REPETIDO
                        bool existe = false;

                        for (int i = 0; i < total; i++)
                        {
                            if (estudiantes[i].idEstudiante == id)
                            {
                                existe = true;
                            }
                        }

                        if (existe)
                        {
                            Console.WriteLine("ERROR: Ya existe un estudiante con ese ID.");
                        }
                        else
                        {
                            double[] notasTemp = new double[5];

                            Console.WriteLine("Ingrese las 5 notas (0 a 20):");

                            for (int i = 0; i < 5; i++)
                            {
                                double nota;

                                Console.Write($"Nota {i + 1}: ");
                                nota = LeerNotas(0, 20);
                                notasTemp[i] = nota;
                            }
                            estudiantes[total] = new Estudiante(nom, id, notasTemp);
                            estudiantes[total].CalcularPromedio();
                            total++;
                            Console.WriteLine("Estudiante registrado correctamente.");
                        }
                    }
                    break;

                case 2:

                    if (total == 0)
                    {
                        Console.WriteLine("ERROR: Debe registrar al menos un estudiante primero.");
                    }
                    else
                    {
                        // CALCULAR PROMEDIOS
                        for (int i = 0; i < total; i++)
                        {
                            estudiantes[i].CalcularPromedio();
                        }

                        Console.WriteLine("¿Como desea ver los resultados?");
                        Console.WriteLine("1. Ordenar por promedio (mayor a menor)");
                        Console.WriteLine("2. Ordenar por nombre (A - Z)");
                        Console.WriteLine("3. Sin ordenar");

                        int orden = LeerEnteros(1, 3);

                        // ORDENAR
                        for (int i = 0; i < total - 1; i++)
                        {
                            for (int j = 0; j < total - 1 - i; j++)
                            {
                                bool cambiar = false;

                                if (orden == 1)
                                {
                                    if (estudiantes[j].promedio < estudiantes[j + 1].promedio)
                                    {
                                        cambiar = true;
                                    }
                                }
                                else if (orden == 2)
                                {
                                    if (String.Compare(estudiantes[j].nombre, estudiantes[j + 1].nombre) > 0)
                                    {
                                        cambiar = true;
                                    }
                                }
                                if (cambiar)
                                {
                                    Estudiante temp = estudiantes[j];
                                    estudiantes[j] = estudiantes[j + 1];
                                    estudiantes[j + 1] = temp;
                                }
                            }
                        }

                        Imprimir(estudiantes, total);
                    }

                    break;

                case 3:
                    if (total == 0)
                    {
                        Console.WriteLine("ERROR: No hay estudiantes registrados.");
                    }
                    else
                    {
                        Console.WriteLine("¿Como desea buscar?");
                        Console.WriteLine("1. Por nombre");
                        Console.WriteLine("2. Por ID");

                        int criterioBusqueda = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Ingrese el texto a buscar: ");

                        string textoBusqueda = Console.ReadLine().ToUpper();

                        bool encontrado = false;

                        Console.WriteLine();
                        Console.WriteLine("RESULTADOS DE BUSQUEDA");
                        Console.WriteLine();

                        for (int i = 0; i < total; i++)
                        {
                            if (criterioBusqueda == 1)
                            {
                                if (estudiantes[i].nombre.Contains(textoBusqueda))
                                {
                                    Console.WriteLine("Nombre   : " + estudiantes[i].nombre);
                                    Console.WriteLine("ID       : " + estudiantes[i].idEstudiante);
                                    Console.WriteLine("Promedio : " + estudiantes[i].promedio);
                                    Console.WriteLine("Estado   : " + estudiantes[i].ObtenerEstado());
                                    Console.WriteLine("--------------------------------");

                                    encontrado = true;
                                }
                            }
                            else if (criterioBusqueda == 2)
                            {
                                if (estudiantes[i].idEstudiante == textoBusqueda)
                                {
                                    Console.WriteLine("Nombre   : " + estudiantes[i].nombre);
                                    Console.WriteLine("ID       : " + estudiantes[i].idEstudiante);
                                    Console.WriteLine("Promedio : " + estudiantes[i].promedio);
                                    Console.WriteLine("Estado   : " + estudiantes[i].ObtenerEstado());
                                    Console.WriteLine("--------------------------------");

                                    encontrado = true;
                                }
                            }
                        }

                        if (!encontrado)
                        {
                            Console.WriteLine("No se encontro ningun estudiante.");
                        }
                    }

                    break;

                case 4:

                    if (total == 0)
                    {
                        Console.WriteLine("ERROR: No hay estudiantes registrados.");
                    }
                    else
                    {
                        Console.WriteLine("¿Como desea ordenar la lista?");
                        Console.WriteLine("1. Por nombre (A - Z)");
                        Console.WriteLine("2. Por promedio (mayor a menor)");
                        Console.WriteLine("3. Sin ordenar");

                        int orden = LeerEnteros(1, 3);

                        // ORDENAR
                        for (int i = 0; i < total - 1; i++)
                        {
                            for (int j = 0; j < total - 1 - i; j++)
                            {
                                bool cambiar = false;
                                if (orden == 1)
                                {
                                    if (String.Compare(estudiantes[j].nombre, estudiantes[j + 1].nombre) > 0)
                                    {
                                        cambiar = true;
                                    }
                                }
                                else if (orden == 2)
                                {
                                    if (estudiantes[j].promedio < estudiantes[j + 1].promedio)
                                    {
                                        cambiar = true;
                                    }
                                }

                                if (cambiar)
                                {
                                    Estudiante temp = estudiantes[j];
                                    estudiantes[j] = estudiantes[j + 1];
                                    estudiantes[j + 1] = temp;
                                }
                            }
                        }
                        Console.WriteLine();
                        Imprimir(estudiantes, total);
                        Console.WriteLine();
                        Console.WriteLine("Total de estudiantes: " + total);
                    }

                    break;
                case 5:

                    Console.WriteLine("\nSaliendo\nHasta  pronto");
                    break;

                default:

                    Console.WriteLine("ERROR: Opcion invalida.");
                    break;
            }

        } while (opcion != 5);
    }
}