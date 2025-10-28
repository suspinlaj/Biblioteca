using System.Threading.Tasks;

namespace Biblioteca_SusanaPineros;

public partial class PantallaAlta : ContentPage
{
    public static List<Libro> biblioteca = new List<Libro>();

    public PantallaAlta()
	{
		InitializeComponent();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ResetearPagina();
    }

    private void ResetearPagina()
    { 
        entryTitulo.Text = "";
        entryAutor.Text = "";
        entryEditorial.Text = "";
        imgPortada.Source = "noimagen.jpg";
        imgDatos.Source = "imgdatos.png";
    }

    private async void OnClickSeleccionarImagen(object sender, EventArgs e)
    {
        // Para que solo se pueda elegir imagenes
        var opciones = new PickOptions
        {
            FileTypes = FilePickerFileType.Images
        };

        // Abrir selector de archivos
        var resultado = await FilePicker.PickAsync(opciones);

        if (resultado != null)
        {
            //Mostrar la imagen que se eligio
            imgPortada.Source = ImageSource.FromStream(() =>
            {   
                var stream = resultado.OpenReadAsync().Result;
                return stream;
            });
        }
    }
    private void OnClickLimpiar(object sender, EventArgs e)
    {
        entryTitulo.Text = "";
        entryAutor.Text = "";
        entryEditorial.Text = "";
        imgPortada.Source = "noimagen.jpg";
        imgDatos.Source = "imgdatos.png";
    }
   
    
    private void OnClickGuardar(object sender, EventArgs e)
	{
        // Informar de que falta algún dato cambiando la img de la bibliotecaria
        if ((string.IsNullOrWhiteSpace(entryTitulo.Text)) || (string.IsNullOrWhiteSpace(entryEditorial.Text))
            || (string.IsNullOrWhiteSpace(entryAutor.Text)) || imgPortada.Source.ToString().Contains("noimagen.jpg"))
        {
            imgDatos.Source = "imgdatosmal.png";
        }else
        {
            if(LibroExistente())
            {
                imgDatos.Source = "imglibroexiste.png";
            }
            else
            {
                // Crear el libro si no existe
                var libro = new Libro
                {
                    Titulo = entryTitulo.Text,
                    Autor = entryAutor.Text,
                    Editorial = entryEditorial.Text,
                    Portada = imgPortada.Source

                };
                imgDatos.Source = "imgdatosbien.png";
                DatosLibros.biblioteca.Add(libro);

            }

        }
    }

    // Comprobar que el libro ingresado ya existe en la lista
    private bool LibroExistente()
    {
        foreach(Libro libros in biblioteca)
        {
            if (libros.Titulo.Equals(entryTitulo.Text) && libros.Editorial.Equals(entryEditorial.Text)
                && libros.Autor.Equals(entryAutor.Text))
            {
                return true;
            }
        }
        return false;
    }
}