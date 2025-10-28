namespace Biblioteca_SusanaPineros;

public partial class PantallaConsulta : ContentPage
{
    public static List<Libro>? biblioteca = null;
    private const int TiempoDobleClic = 300; // Tiempo en milisegundos para considerar un doble clic
    private DateTime _ultimaHoraClic;
    private object _ultimoElementoClicado;
    public PantallaConsulta()
	{
		InitializeComponent();
        biblioteca = recuperarLibros();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ResetearPagina();
    }

    private void ResetearPagina()
    {
        rbAutor.IsChecked = false;
        rbEditorial.IsChecked = false;
        TitulosView.ItemsSource = "";
        AutoresEditorialesView.ItemsSource = "";
        imgPortada.Source = "imgPortada";
    }

    private List<Libro> recuperarLibros()
	{
        return DatosLibros.biblioteca;
        
    }

    private void rbCheckedChange(object sender, CheckedChangedEventArgs e)
	{
        var radioButton = sender as RadioButton;
        var listaLibros = recuperarLibros();

        // Coger el texto del radiobutton
        string? rbSeleccionado = radioButton.Content.ToString();
        

        // Comprobar que radioButton se ha selecionado por su texto anterioremente guardado
        if(rbSeleccionado.Equals("Autor"))
        {
            var autores = biblioteca.Select(libro => libro.Autor).Distinct().ToList();
            AutoresEditorialesView.ItemsSource = autores;
        }
        else if (rbSeleccionado.Equals("Editorial"))
        {
            var editoriales = biblioteca.Select(libro => libro.Editorial).Distinct().ToList();
            AutoresEditorialesView.ItemsSource = editoriales;
        }

        TitulosView.ItemsSource = null;
    }

    private void AutoresEditorialView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            string selectedValue = e.SelectedItem.ToString();

            if (rbAutor.IsChecked)
            {
                // Mostrar los títulos por el autor 
                var titulos = biblioteca
                                .Where(libro => libro.Autor == selectedValue)
                                .Select(libro => libro.Titulo)
                                .ToList();
                TitulosView.ItemsSource = titulos;
            }
            else if (rbEditorial.IsChecked)
            {
                // Mostrar los títulos por editorial 
                var titulos = biblioteca
                                .Where(libro => libro.Editorial == selectedValue)
                                .Select(libro => libro.Titulo)
                                .ToList();
                TitulosView.ItemsSource = titulos;
            }
        }
    }

    private void lvTitulo_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var horaActual = DateTime.Now;
        var diferenciaTiempo = (horaActual - _ultimaHoraClic).TotalMilliseconds;

        if (_ultimoElementoClicado == e.Item && diferenciaTiempo <= TiempoDobleClic)
        {
            // Se detectó un doble clic
            _ultimaHoraClic = DateTime.MinValue;
            _ultimoElementoClicado = null;
            AlHacerDobleClicEnElemento(e.Item); // Método para manejar el doble clic
        }
        else
        {
            // Primer clic
            _ultimaHoraClic = horaActual;
            _ultimoElementoClicado = e.Item;
        }
    }

    private void AlHacerDobleClicEnElemento(object elemento)
    {
        if (elemento != null)
        {
            string tituloSeleccionado = elemento.ToString();
            var portada = biblioteca.Where(libro => libro.Titulo == tituloSeleccionado).Select(libro => libro.Portada);

                // Cargar la imagen de la portada
                imgPortada.Source = portada.FirstOrDefault();
           
        }
    }

}