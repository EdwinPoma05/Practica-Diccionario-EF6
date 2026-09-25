// See https://aka.ms/new-console-template for more information
using Diccionario;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
bool validarRepuestoEliminado=true;

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
    Console.WriteLine("6.- Eliminar repuesto");
    Console.WriteLine("7.- Hay Respuestos sin stock");
    Console.WriteLine("8.- Resumen de inventario");


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
                
                do
                {
                    Console.WriteLine("Ingrese el codigo a eliminar");
                    string? codigoIngresadoaEliminar = Console.ReadLine();
                    Repuesto? repuestoEncontrado;
                    validarRepuestoEliminado = TryObtenerCodigoValidoParaEliminar(codigoIngresadoaEliminar, contexto, out repuestoEncontrado);

                } while (validarRepuestoEliminado);


             break;

            case 7:
                bool existeRepuestoSinStock =HayRespuestosSinStock(contexto); 
                if (existeRepuestoSinStock)
                {
                    Console.WriteLine("Si hay repuestos con stock 0");
                }
                else
                {
                    Console.WriteLine("No hay repuestos con stock 0");

                }

                break;

            case 8:
                ResumenInventario resumen = ResumenDeInventario(contexto);
                Console.WriteLine($"Tenemos {resumen.CantidadRepuestos} tipos de repuestos");
                Console.WriteLine($"Tenemos {resumen.stockTotal} en total ");
                Console.WriteLine($"Tenemos un promedio de {resumen.PromedioStockGeneral} de stock en totla");
                Console.WriteLine($"El repuesto que tiene stock mayor es {resumen.StockMayor.Nombre} con {resumen.StockMayor.Stock} de stock");
                Console.WriteLine($"El repuesto que tiene menor stock es {resumen.StockMenor.Nombre} con {resumen.StockMenor.Stock}");
                break;
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
            if (respuestoBuscado.Estado==true)
            {
                Console.WriteLine("Se encontro el repuesto");
                Console.WriteLine($"Nombre: {respuestoBuscado.Nombre} Stock: {respuestoBuscado.Stock} Precio: {respuestoBuscado.Precio}");
            }
            else
            {
                Console.WriteLine("No se encontro el repuesto");
                
            }

        }
        else
        {
            Console.WriteLine("No se encontro el repuesto");
        }
    }
}



void ListarRepuestos(TallerContext contextoTaller)
{
   List<Repuesto> listaRepuestos = contextoTaller.Repuestos.Where(r=>r.Estado==true).OrderBy(r=>r.Nombre).ToList();

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
        if (repuestoEncontrado.Estado == true)
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
        return true;
    }
    string codigoNormalizado = codigoIngresado.ToUpper().Trim();
    Repuesto? respuestoaEliminarEnContrado= contextoTaller.Repuestos.FirstOrDefault(r => r.Codigo.ToUpper().Trim() == codigoNormalizado && r.Estado==true);

    if(respuestoaEliminarEnContrado!= null)
    {
        respuestoaEliminarEnContrado.Estado = false;
        contextoTaller.SaveChanges();
        Console.WriteLine("Repuesto eliminado correctamente");
        respuestoEncontrado= respuestoaEliminarEnContrado;
        return false;

    }

    Console.WriteLine("No se encontro el repuesto");
    respuestoEncontrado = null;
    return true;
}
bool HayRespuestosSinStock(TallerContext contexto)
{
    return contexto.Repuestos.Any(r => r.Stock == 0 && r.Estado == true);
}
int CantidadRepuestosActivo (TallerContext contexto)
{
    return contexto.Repuestos.Count(r=>r.Estado==true);
}

int CantidadStockTotal (TallerContext contexto)
{
    return contexto.Repuestos.Where(r=>r.Estado==true).Sum(r=>r.Stock);
}

void OrdenDescendeteStockPorRepuesto(TallerContext contexto)
{
    List<Repuesto> lista = contexto.Repuestos.Where(r=>r.Estado==true).OrderByDescending(r => r.Stock).ToList();
    
    for(int i = 0; lista.Count > i; i++)
    {
        Console.WriteLine($"{lista[i].Codigo}");
        Console.WriteLine($"{lista[i].Stock}");
    }
    
}

void NombreRepuestos(TallerContext contexto)
{
     List<string> listaNombres =contexto.Repuestos.Where(r=>r.Estado==true).OrderBy(r=>r.Nombre).Select(r => r.Nombre).ToList();
    
     for(int i = 0; listaNombres.Count > i; i++)
    {
        Console.WriteLine($"{listaNombres[i]}");
    }
    
}

void NombresYStock(TallerContext contexto)
{
   var lista= contexto.Repuestos.Where(r => r.Estado == true).OrderBy(r => r.Nombre).Select(r=>new {r.Nombre,r.Stock }).ToList();

    for(int i = 0; lista.Count > i; i++)
    {
        Console.WriteLine($"{lista[i].Nombre}");
        Console.WriteLine($"{lista[i].Stock}");
    }
}


Repuesto? RepuestoConMayorStock(TallerContext contexto)
{
    Repuesto? RepuestoMayorStock= contexto.Repuestos.Where(r => r.Estado == true).OrderByDescending(r => r.Stock).FirstOrDefault();
    return RepuestoMayorStock;
}

Repuesto? RepuestoConMenorStock(TallerContext contexto)
{
    Repuesto? repuestoMenorStock = contexto.Repuestos.Where(r => r.Estado == true).OrderBy(r => r.Stock).FirstOrDefault();
    return repuestoMenorStock;
}

double PromedioStock (TallerContext contexto)
{
    if (contexto.Repuestos.Any(r => r.Estado))
    {
        return contexto.Repuestos.Where(r => r.Estado == true).Average(r => r.Stock);
    }
    return 0;
    
}

ResumenInventario ResumenDeInventario(TallerContext contexto)
{
    if (contexto.Repuestos.Where(r => r.Estado).Any())
    {
        int repuestosActivos=contexto.Repuestos.Where(r => r.Estado).Count();
        int tockGeneral=contexto.Repuestos.Where(r => r.Estado).Sum(r => r.Stock);
        double promedioStock=contexto.Repuestos.Where(r => r.Estado).Average(r => r.Stock);
        Repuesto ?stockMayor=contexto.Repuestos.Where(r => r.Estado).OrderByDescending(r => r.Stock).FirstOrDefault();
        Repuesto? stockMenor=contexto.Repuestos.Where(r => r.Estado).OrderBy(r => r.Stock).FirstOrDefault();

        ResumenInventario resumen = new ResumenInventario
        {
            CantidadRepuestos = repuestosActivos,
            stockTotal = tockGeneral,
            PromedioStockGeneral = promedioStock,
            StockMayor = stockMayor,
            StockMenor = stockMenor,
        };

        return resumen;
        
    }
    else
    {
        return new ResumenInventario();
    }

    
}