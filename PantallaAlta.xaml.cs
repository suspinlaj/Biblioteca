using System.Threading.Tasks;

namespace Biblioteca_SusanaPineros;

public partial class PantallaAlta : ContentPage
{
    static List<Libro> biblioteca = new List<Libro>();

    public PantallaAlta()
	{
		InitializeComponent();

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
                var libro = new Libro
                {
                    Titulo = entryTitulo.Text,
                    Autor = entryAutor.Text,
                    Editorial = entryEditorial.Text,
                    Portada = imgPortada.Source

                };
                imgDatos.Source = "imgdatosbien.png";
            }
                
        }
    }

    private bool LibroExistente()
    {
        bool existe = false;

        foreach(Libro libros in biblioteca)
        {
            if (libros.Titulo.Equals(entryTitulo) && libros.Editorial.Equals(entryEditorial)
                && libros.Autor.Equals(entryAutor))
            {
                existe = true;
            }
        }
        return existe;
    }
}