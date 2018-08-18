namespace ProyectoTelefonia
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Web.Mvc;

    public class ModelDB : DbContext
    {
        // El contexto se ha configurado para usar una cadena de conexión 'ModelDB' del archivo 
        // de configuración de la aplicación (App.config o Web.config). De forma predeterminada, 
        // esta cadena de conexión tiene como destino la base de datos 'ProyectoTelefonos.ModelDB' de la instancia LocalDb. 
        // 
        // Si desea tener como destino una base de datos y/o un proveedor de base de datos diferente, 
        // modifique la cadena de conexión 'ModelDB'  en el archivo de configuración de la aplicación.
        public ModelDB() : base("name=ModelDB")
        {
        }

        // Agregue un DbSet para cada tipo de entidad que desee incluir en el modelo. Para obtener más información 
        // sobre cómo configurar y usar un modelo Code First, vea http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<Interno> Interno { get; set; }
        public DbSet<Directo> Directo { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<SubArea> SubArea { get; set; }
        public DbSet<Puesto> Puesto { get; set; }
        public DbSet<Piso> Piso { get; set; }
        public DbSet<Contacto> Contacto { get; set; }
    }


    [Table("Interno")]
    public class Interno
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un {0} de 4 digitos")]
        [Range(0, 6099, ErrorMessage = "Debe ingresar un rango entre {1} a {2}")]
        [Remote("ExisteInterno", "Internos", AdditionalFields = "Id", ErrorMessage = "El {0} ya existe")]
        public long Numero { get; set; }

        public string Tipo { get; set; }

        [Remote("ExisteTn", "Internos", AdditionalFields = "Id", ErrorMessage = "El {0} ya existe")]
        public string Tn { get; set; }

        [Display(Name = "Puesto Telefonico")]
        public string PuestoTel { get; set; }

        public string Estado { get; set; }

        [Display(Name = "No Mostrar")]
        public bool NoMostrar { get; set; }

        [StringLength(500, ErrorMessage = "Ha superado la cantidad maxima de {1} caracteres")]
        [DataType(DataType.MultilineText)]
        public string Observacion { get; set; }

        [ForeignKey("SubArea")]
        [Column("SubArea_id")]
        public virtual long SubArea_id { get; set; }
        public virtual SubArea SubArea { get; set; }

        [ForeignKey("Puesto")]
        [Column("Puesto_id")]
        public virtual long Puesto_id { get; set; }
        public virtual Puesto Puesto { get; set; }
    }


    [Table("Directo")]
    public class Directo
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un {0}")]
        [Display(Name = "Numero de Telefono")]
        [DataType(DataType.PhoneNumber)]
        public string Numero { get; set; }

        public string Estado { get; set; }
        public bool NoMostrar { get; set; }
        public string Observacion { get; set; }

        [ForeignKey("SubArea")]
        [Column("SubArea_id")]
        public virtual long SubArea_id { get; set; }
        public virtual SubArea SubArea { get; set; }

        [ForeignKey("Puesto")]
        [Column("Puesto_id")]
        public virtual long Puesto_id { get; set; }
        public virtual Puesto Puesto { get; set; }
    }


    [Table("Area")]
    public class Area
    {
        public Area()
        {
            SubAreas = new Collection<SubArea>();
        }

        public long Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un {0}")]
        public string Nombre { get; set; }

        public virtual ICollection<SubArea> SubAreas { get; set; }
    }


    [Table("SubArea")]
    public class SubArea
    {
        public SubArea()
        {
            Internos = new Collection<Interno>();
            Pisos = new Collection<Piso>();
        }

        public long Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un {0}")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar un {0}")]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }

        public string Referente { get; set; }

        public virtual ICollection<Interno> Internos { get; set; }
        public virtual ICollection<Directo> Directos { get; set; }
        public virtual ICollection<Piso> Pisos { get; set; }

        [ForeignKey("Area")]
        [Column("Area_id")]
        public virtual long Area_id { get; set; }
        public virtual Area Area { get; set; }
    }


    [Table("Puesto")]
    public class Puesto
    {
        public Puesto()
        {
            Internos = new Collection<Interno>();
        }

        public long Id { get; set; }
        public string NumeroTipo { get; set; }

        public virtual ICollection<Interno> Internos { get; set; }
    }


    [Table("Piso")]
    public class Piso
    {
        public Piso()
        {
            SubAreas = new Collection<SubArea>();
        }

        public long Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un {0}")]
        public long Numero { get; set; }

        public virtual ICollection<SubArea> SubAreas { get; set; }
    }


    [Table("Contacto")]
    public class Contacto
    {
        public long Id { get; set; }

        [HiddenInput]
        [DataType(DataType.DateTime)]
        public string Fecha { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Correo electrónico")]
        [EmailAddress]
        public string Email { get; set; }

        public string Estado { get; set; }

        [Display(Name = "Sugerencia")]
        [StringLength(500, ErrorMessage = "Ha superado la cantidad maxima de {1} caracteres")]
        [DataType(DataType.MultilineText)]
        public string Observacion { get; set; }
    }


}



//1. – Borrar el directorio Migrations de tu proyecto.
//2. – Borrar la tabla _MigrationsHistory de tu base de datos.
//3. – Ejecutar el siguiente comando en la consola de administración de paquetes.

//Enable-Migrations -EnableAutomaticMigrations -Force

//4. – Finalmente ejecutar:

//Add-Migration Initial
