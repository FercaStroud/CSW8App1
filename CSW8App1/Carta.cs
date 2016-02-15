using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace CSW8App1
{
    public class Carta 
    {
        bool bandera;
        int valor;
        int pos;
        Image imagen = new Image();
        BitmapImage biCartaVacia = new BitmapImage();
        public Carta(Grid contenedor)
        {
            imagen.Width = 78;
            imagen.Height = 104;
            imagen.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            imagen.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
            imagen.Visibility = Windows.UI.Xaml.Visibility.Visible;
            
            contenedor.Children.Add(imagen);
        }
        public void setEvent(TappedEventHandler e)
        {
            imagen.Tapped += new TappedEventHandler(e);
        }
        public void setSource(Uri uri)
        {
            imagen.Source = new BitmapImage(uri); 
        }
        public void setMargin(Thickness x)
        {
            imagen.Margin = x;
        }
        public void setName(String name)
        {
            imagen.Name = name;
        }
        public Image Imagen
        {
            get { return imagen; }
            set { imagen = value; }
        }
        public bool Bandera 
        { 
            get { return bandera; }
            set { bandera = value; }
        }
        public int Valor
        {
            get { return valor; }
            set { valor = value; }
        }
        public int Pos
        {
            get { return pos; }
            set { pos = value; }
        }

    }
}
