// See https://aka.ms/new-console-template for more information
using Diccionario;
using System.ComponentModel.DataAnnotations;

Repuesto respuesto = new Repuesto
{
    Codigo="ABC123",
    Nombre="Llantas",
    Stock=10,
    Precio=10.25m,
    Estado= true
};

TallerContext contexto = new TallerContext();

//contexto.Repuestos.Add(respuesto);


//contexto.SaveChanges();

List<Repuesto> ListaRepuestos =contexto.Repuestos.ToList();

//Console.WriteLine($"{ListaRepuestos[0]}");



Dictionary<string, Repuesto> diccionarioRepuesto = new Dictionary<string, Repuesto>();
for (int i = 0;i< ListaRepuestos.Count; i++)
{
   
    if (diccionarioRepuesto.ContainsKey(ListaRepuestos[i].Codigo.ToUpper().Trim()))
    {
        Console.WriteLine("Ya existe un repuesto con el mismo código");
    }
    else
    {
        Console.WriteLine($"{ListaRepuestos[i].Nombre} {ListaRepuestos[i].Codigo}");
        diccionarioRepuesto.Add(ListaRepuestos[i].Codigo.ToUpper().Trim(), ListaRepuestos[i]);
    }
}

bool continuarPrograma = true;
bool validadorcodigo = true;
bool validadorNombre = true;
bool validadorStock = true;
bool validadorPrecio = true;

do
{


    Console.WriteLine("Bievenido al sistema de repuestos");
    Console.WriteLine("================================");
    Console.WriteLine("Por favor ingrese la opcion que desea realizar");
    Console.WriteLine("1.- Buscar repuesto");
    Console.WriteLine("2.- Agregar repuesto");
    Console.WriteLine("3.- Listar Respuestos");
    Console.WriteLine("4.- Salir");
    Console.WriteLine("5.- Actualizar stock de repuesto");


    string? opcionIntroducida = Console.ReadLine();

    bool validador = true;
    int opcionSeleccionada;
    string codigoValido = "";
    string nombreValido = "";
    int stockValido = 0;
    decimal precioValido = 0;

    if (int.TryParse(opcionIntroducida, out opcionSeleccionada))
    {
        switch (opcionSeleccionada)
        {
            case 1:



                Console.WriteLine("Ingrese el código del repuesto a buscar");
                
                string? codigo = Console.ReadLine();

                BuscarRepuestos(codigo, diccionarioRepuesto);
                
                break;

            case 2:

                string codigoValidoTemp;

                do
                {
                    Console.WriteLine("Ingrese el codigo del Respuesto a agregar");
                    string? codigoIngresado = Console.ReadLine();


                    validadorcodigo = TryObtenerCodigoValido(codigoIngresado, diccionarioRepuesto, out codigoValidoTemp);


                } while (validadorcodigo);
                do
                {
                    Console.WriteLine("Ingrese el nombre del repuesto a agregar");
                    string? nombreIngresado = Console.ReadLine();
                   validadorNombre = TryObtenerNombreValido(nombreIngresado, out nombreValido);


                } while (validadorNombre);

                do
                {
                    Console.WriteLine("Ingrese el stock del repuesto a agregar");
                    string? stockIngresado = Console.ReadLine();
                    validadorStock = TryObtenerStockValido(stockIngresado, out stockValido);

                } while (validadorStock);


                do
                {
                    Console.WriteLine("Ingrese el precio del repuesto a agregar");
                    string? precioIngresado = Console.ReadLine();
                    validadorPrecio  = TryObtenerPrecioValido(precioIngresado, out precioValido);
                } while (validadorPrecio);

                Repuesto nuevoRespuesto = new Repuesto
                {
                    Codigo = codigoValidoTemp,
                    Nombre = nombreValido,
                    Stock = stockValido,
                    Precio = precioValido,
                    Estado = true
                };

                contexto.Repuestos.Add(nuevoRespuesto);
                contexto.SaveChanges();
              
                diccionarioRepuesto.Add(codigoValidoTemp, nuevoRespuesto);

                Console.WriteLine("Repesto agregado correctamente");
                Console.WriteLine($"Codigo:{codigoValidoTemp}");
                Console.WriteLine($"Nombre:{nombreValido}");
                Console.WriteLine($"Stock:{stockValido}");
                Console.WriteLine($"Precio:{precioValido}");

                
                break;

            case 3:
                Console.WriteLine("Listado de repuestos");
                Console.WriteLine("====================");
                ListarRepuestos(contexto);
                break;

            case 4:

                continuarPrograma = false;
                break;

            case 5:

                Console.WriteLine("Ingrese el codigo de repuesto a actualizar su stock");
                string? codigoIngresadoaEditar=Console.ReadLine();
                ActualizarStock(codigoIngresadoaEditar, diccionarioRepuesto, contexto);

                break;
            case 6:
                Console.WriteLine("Ingrese el codigo a eliminar");
                string? codigoIngresadoaEliminar = Console.ReadLine();

                TryObtenerCodigoValidoParaEliminar()



            default:
                Console.WriteLine("Ingrese una opcion valida");
                continuarPrograma = true;
                break;


        }

    }
    else
    {
        Console.WriteLine("Ingrese una opcion valida");
        continuarPrograma = true;
    }
} while (continuarPrograma);



void BuscarRepuestos(string? codigoBuscado, Dictionary<string, Repuesto> diccionarioRepuesto)
{
    Repuesto respuestoBuscado = new Repuesto();
   
    if (string.IsNullOrWhiteSpace(codigoBuscado))
    {
        Console.WriteLine("El código no puede estar vacío");
    }
    else
    {
        string codigoNormalizado = codigoBuscado.ToUpper().Trim();
        if (diccionarioRepuesto.TryGetValue(codigoNormalizado, out respuestoBuscado))
        {
            Console.WriteLine("Se encontro el repuesto");
            Console.WriteLine($"Nombre: {respuestoBuscado.Nombre} Stock: {respuestoBuscado.Stock} Precio: {respuestoBuscado.Precio}");
        }
        else
        {
            Console.WriteLine("No se encontro el repuesto");
        }
    }
}



void ListarRepuestos(TallerContext contextoTaller)
{
   List<Repuesto> listaRepuestos = contextoTaller.Repuestos.ToList();

    for (int i = 0; listaRepuestos.Count > i; i++)
    {
        Console.WriteLine($"{listaRepuestos[i].Nombre}");
        Console.WriteLine($"{listaRepuestos[i].Codigo}");
        Console.WriteLine($"{listaRepuestos[i].Stock}");
        Console.WriteLine($"{listaRepuestos[i].Precio}");
    }

}

void ActualizarStock(string? codigoIngresadoaEditar, Dictionary<string,Repuesto> diccionarioRepuesto, TallerContext contexto )
{
    if (string.IsNullOrWhiteSpace(codigoIngresadoaEditar))
    {
        Console.WriteLine("Ingrese un codigo valido");
    }
    else if (diccionarioRepuesto.TryGetValue(codigoIngresadoaEditar.ToUpper().Trim(), out Repuesto repuestoEncontrado))
    {
        Console.WriteLine("Ingrese el nuevo stock del repuesto");

        if (int.TryParse(Console.ReadLine(), out int stockActualizado))
        {
            if (stockActualizado >= 0)
            {
                Console.WriteLine($"Stock anterior {repuestoEncontrado.Stock}");
                repuestoEncontrado.Stock = stockActualizado;
                contexto.SaveChanges();
                Console.WriteLine($"Stock actualizado {repuestoEncontrado.Stock}");
                Console.WriteLine("Stock actualizado correctamente");
            }
            else
            {
                Console.WriteLine("Ingrese un stock mayor o igual a 0");
            }
        }
        else
        {
            Console.WriteLine("Ingrese un stock valido");
        }

    }
    else
    {
        Console.WriteLine("No se encontro el repuesto");
    }
}


bool TryObtenerCodigoValido(string? codigoNuevo,Dictionary<string,Repuesto> diccionarioRepuesto, out string codigoValido)
{
    if (string.IsNullOrWhiteSpace(codigoNuevo))
    {
        Console.WriteLine("Ingrese codigo valido");
        codigoValido = "";
        return true;
    }
    string codigoNormalizado = codigoNuevo.ToUpper().Trim();
    if (diccionarioRepuesto.ContainsKey(codigoNormalizado))
    {
        Console.WriteLine("El codigo ya esta registrado");
        codigoValido = "";
        return true;
    }
        codigoValido = codigoNormalizado;
        return false;
}


bool TryObtenerNombreValido(string?nombreIngresado, out string nombreValido)
{
    if (string.IsNullOrWhiteSpace(nombreIngresado))
    {
        Console.WriteLine("Ingrese un nombre valido");
        nombreValido = "";
        return true;
    }
    string nombreNormalizado= nombreIngresado.ToUpper().Trim();
    nombreValido = nombreNormalizado;
    return false;
}

bool TryObtenerStockValido(string?stockIngresado, out int stockValido)
{
    if (int.TryParse(stockIngresado, out int stockConvertido))
    {
        if (stockConvertido > 0)
        {
            stockValido = stockConvertido;
            return false;
        }
        else
        {
            Console.WriteLine("Ingrese un stock mayor a 0");
            stockValido = 0;
            return true;
        }
    }
    else
    {
        Console.WriteLine("Ingrese un stock valido");
        stockValido = 0;
        return true;
    }

}

bool TryObtenerPrecioValido(string? precioIngresado, out decimal precioValido)
{
   if(decimal.TryParse(precioIngresado, out decimal precioConvertido))
    {
        if (precioConvertido > 0)
        {
            precioValido = precioConvertido;
            return false;
        }
        else
        {
            Console.WriteLine("Ingrese un precio mayor a 0");
            precioValido = 0;
            return true;
        }
    }
    else
    {
        Console.WriteLine("Ingrese un precio valido");
        precioValido = 0;
        return true;
    }
}

bool TryObtenerCodigoValidoParaEliminar(string ? codigoIngresado,TallerContext contextoTaller, out Repuesto? respuestoEncontrado )
{
    if (string.IsNullOrWhiteSpace(codigoIngresado))
    {
         Console.WriteLine("Ingrese un codigo valido");
        respuestoEncontrado = null;
        return false;
    }
    string codigoNormalizado = codigoIngresado.ToUpper().Trim();

    List<Repuesto> listaRepuestos = contextoTaller.Repuestos.ToList();

    for (int i = 0; listaRepuestos.Count > i; i++)
    {
        if (listaRepuestos[i].Codigo == codigoNormalizado)
        {
            listaRepuestos[i].Estado = false;
            contextoTaller.SaveChanges();
            Console.WriteLine("Repuesto eliminado correctamente");
            respuestoEncontrado = listaRepuestos[i];
            return false;
        }
       
    }
    respuestoEncontrado = null;
    return true;
}


