using TaskManagementLibrary; //RETO 1 (1 pt): La linea está bien, pero no funciona, corregir (Se le agregó una referencia a la librería) (Listo) (Se le hizo la referencia a la librería)

class Program
{
    static bool Confirme(string accion)
    {
        Console.WriteLine("Confirme " + accion + " s/n");
        return Console.ReadLine() == "s";
    } 
     static void Main(string[] args)
    {
        var taskService = new TaskService();

        while (true)
        {
            Console.WriteLine("1. Agregar tarea");
            Console.WriteLine("2. Ver tareas");
            Console.WriteLine("3. Actualizar tarea");
            Console.WriteLine("4. Eliminar tarea");
            Console.WriteLine("5. Completar tarea");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Write("Titulo: ");
                    var title = Console.ReadLine();  
                    if (string.IsNullOrWhiteSpace(title)) title = null;  

                    Console.Write("Descripcion: ");
                    var description = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(description)) description = null;

                    /* RETO 2 (3 pts) : previo a agregar la tarea, verificar que 
                    los datos no sean solo espacios en blanco, de ser así 
                    asignar nulo al dato ( title o description) (Listo) (Se le añadió la condicional "if" en caso de que el dato introducido por el usuario sea nulo, se le rellenara con el valor "NULL")
                    */
                    
                    var task = taskService.AddTask(title, description);
                    Console.WriteLine($"Tarea agregada con Id: {task.Id}");
                    break;
                case "2":
                    var tasks = taskService.GetAllTasks();
                    Console.WriteLine("-------------------------------------------------");
                    foreach (var t in tasks)
                    {
                        Console.WriteLine($"ID: {t.Id}, Titulo: {t.Title}, Descripcion: {t.Description}, Completada: {t.IsCompleted}");
                    }
                    Console.WriteLine("-------------------------------------------------");
                    break;
                case "3":
                    Console.Write("Introduzca el Id de la tarea por actualizar: ");
                    var updateId = int.Parse(Console.ReadLine());
                      task = taskService.GetTaskById(updateId);
                    // RETO 3 (2 pts): corregir, debe cargarse con la tarea que posea el id indicado en updateId (Listo) (Se utlizo el metodo "Get" para obtener los datos de la tarea de acuerdo a su "ID")
                   
                   
                    //RETO 4 (1 pt): imprimir el titulo de la tarea seleccionada(Listo) (Como ya se había obtenido el metod "Get" simplemente se le indicó que imprimiera en pantalla el valor del titulo y la descripcion con su respectivo dato)
                   

                    Console.Write($"{task.Title} -> Nuevo titulo: ");
                    var newTitle = Console.ReadLine();
                    
                    //RETO 5 (1 pt): imprimir la descripcion de la tarea seleccionada(Listo) (Lo mismo que en el reto 4, solo que ahora la descripcion)
                    Console.Write($"{task.Description} -> Nueva Descripcion: ");
                    var newDescription = Console.ReadLine();
                    Console.Write("Completada (true/false): ");
                    var isCompleted = bool.Parse(Console.ReadLine());
                    
                    /*RETO 6 ( 5 pts ) El código debe modificarse en la librería, de tal forma que si se recibe title vacio
                     entonces no se modifique, lo mismo para description (Listo) (En la librería, al archivo "TaskServices.cs" se le agregó de que en dado caso que el nuevo dato ingresado por el usuario
                     sea nulo, entonces no afectaría los datos actuales*/
                    if (taskService.UpdateTask(updateId, newTitle, newDescription, isCompleted))
                    {
                        Console.WriteLine("Tarea completada exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("Tarea no encontrada.");
                    }
                    break;
                case "4":
    Console.Write("Introduzca el Id de la tarea a eliminar: ");
    var deleteId = 0;
    try
    {
        deleteId = int.Parse(Console.ReadLine());
    }
    catch (FormatException)
    {
        Console.WriteLine("Error: El Id debe ser un número entero.");
        break;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error inesperado: {ex.Message}");
        break;
    }
                    
                    task = taskService.GetTaskById(deleteId); //RETO 7 (2 pts): La linea está bien, pero por alguna razón no se puede realizar el llamado de la función (Listo)
                    
                    if (task == null)
    {
        Console.WriteLine("No se encontró ninguna tarea con el Id especificado.");
        break;
    }
                    Console.WriteLine("Tarea:");
                    Console.Write("     - ");
                    Console.WriteLine(task.Title);
                    if (Confirme("eliminar"))
                    {
                        if (taskService.DeleteTask(deleteId))
                        {
                            Console.WriteLine("Tarea eliminada exitosamente.");
                        }
                        else
                        {
                            Console.WriteLine("Tarea no encontrada.");
                        }
                    }    
                    break;
                case "5":
                    Console.Write("Introduzca el Id de la tarea a completar: ");
                    var completeId = int.Parse(Console.ReadLine());
                    task = taskService.GetTaskById(completeId);

                         if (task == null)
                            {
                                Console.WriteLine("Tarea no encontrada.");
                                break;
                            }

                                Console.WriteLine($"Tarea: {task.Title}");

                         if (taskService.CompleteTask(completeId))
                            {
                                 Console.WriteLine("Tarea completada exitosamente.");
                             }
                         else
                             {
                                Console.WriteLine("No se pudo completar la tarea.");
                            }
//Reto 8 (Listo) (Se hizo un método entero para cambiar el valor del estado de la tarea, en dado caso de que la tarea no sea completada, este hará que la tarea se muestre como completada)
                    break;   
                case "6":
                    return;
                default:
                    Console.WriteLine("Opcion invalida, intente de nuevo.");
                    break;
                    
            }
        }
    }
}
