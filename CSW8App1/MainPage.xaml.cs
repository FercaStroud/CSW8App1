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
    public sealed partial class MainPage : Page
    {
        int contadorO = 20;
        int contadorJ = 20;
        int timer3Stop = 20;
        Random r = new Random();
        Carta[] cartas = new Carta[13];
        int[] varOponente = new int[4];
        BitmapImage biCartaVacia = new BitmapImage();
        private DispatcherTimer timer1, timer2, timer3, timer4;
        TextBlock[] lbFlechas = new TextBlock[4];
        Boolean[] boolBandera = new Boolean[4];

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Form_Load()
        {
            int y = 32,
                x = 230,
                incremento = 110;

            for (int i = 0; i <cartas.Length; i++)
            {
                cartas[i] = new Carta(Contenedor);
                if (i == 0)
                {
                    cartas[i].setName("CartaTemporal");
                    cartas[i].setMargin(new Thickness(880, 520, 0, 0)); 
                }
                else if (i <= 4)
                {
                    cartas[i].setName("CartaO" + i);
                    cartas[i].setMargin(new Thickness((x + incremento), y, 0, 0));
                    incremento += 110;
                    if (i == 4)
                    {
                        incremento = 110;
                        y = 232;
                    }
                }
                else if (i > 4 && i <= 8)
                {
                    cartas[i].setName("CartaP-" + i);
                    cartas[i].setMargin (new Thickness((x + incremento), y, 0, 0));
                    incremento += 110;
                    if (i == 8)
                    {
                        incremento = 110;
                        y = 520;
                    }
                }
                else
                {
                    cartas[i].setName ("CartaJ-" + i);
                    cartas[i].setMargin(new Thickness((x + incremento), y, 0, 0));
                    incremento += 110;
                }
                cartas[i].setSource(new Uri(this.BaseUri, @"Assets/Carta0.jpg"));
                cartas[i].Pos = i;
                cartas[i].setEvent(MovimientoJugador);
               
            }

            x = 334; incremento = 0;
            for (int i = 0; i < lbFlechas.Length; i++)
            {
                lbFlechas[i] = new TextBlock();
                lbFlechas[i].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                lbFlechas[i].VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                lbFlechas[i].Margin = new Thickness((x + incremento), 340, 0, 0);
                lbFlechas[i].Text = "+";
                lbFlechas[i].TextWrapping = TextWrapping.Wrap;
                lbFlechas[i].FontSize = 110;
                incremento += 110;
                Contenedor.Children.Add(lbFlechas[i]);
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        public void RandOponente()
        {
            for (int i = 0; i < 4; i++)
                varOponente[i]= r.Next(1, 14);
        }

        public void RandPozo()
        {
            for (int i = 5; i <= 8; i++)
                Asignar(i);
        }

        public void RandUser()
        {
            for (int i = 9; i <= 12; i++)
                Asignar(i);
        }

        private void Asignar(int i)
        {
            cartas[i].Valor = r.Next(1, 14);
            cartas[i].setSource(new Uri(this.BaseUri, @"Assets/Carta" + cartas[i].Valor + ".jpg"));
            cartas[i].Pos = i;
        }

        private void btnRepartir_Click(object sender, RoutedEventArgs e)
        {
            Form_Load();
            instrucciones.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            btnRepartir.IsEnabled = false;
            RandOponente();
            RandPozo();
            RandUser();
            StartTimers();
            lbJugador.Text = "Restan: " + contadorJ.ToString();
            lbOponente.Text = "Restan: " + contadorO.ToString();
        }

        private void StoptTimers()
        {
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            timer4.Stop();
        }

        private void StartTimers()
        {
            timer1 = new DispatcherTimer();
            timer1.Interval = new TimeSpan(0, 0, 0, 3);
            timer1.Tick += timer1_Tick;
            timer1.Start();

            timer2 = new DispatcherTimer();
            timer2.Interval = new TimeSpan(0, 0, 0, 2);
            timer2.Tick += timer2_Tick;
            timer2.Start();

            timer3 = new DispatcherTimer();
            timer3.Interval = new TimeSpan(0, 0, 0, 0, 80);
            timer3.Tick += timer3_Tick;
            timer3.Start();

            timer4 = new DispatcherTimer();
            timer4.Interval = new TimeSpan(0, 0, 0, 6);
            timer4.Tick += timer4_Tick;
            timer4.Start();
        }

        void MovimientoJugador(object sender, TappedRoutedEventArgs e)
        {
            if (contadorJ == 1)
            {
                lbJugador.Text = "GANASTE";
                lbOponente.Text = "GANASTE";
                StoptTimers();
            }
            else
            {
                Image AUX = ((Image)sender);
                string strAUX = AUX.Name, strPos = "";
                char[] delimitador = new char[] { '-' };
                foreach (string substr in strAUX.Split(delimitador))
                {
                    strPos = substr;
                }
                int pos = int.Parse(strPos);

                if (pos > 8 && pos < 13)
                {
                    cartas[0].Pos = cartas[pos].Pos;
                    cartas[0].Valor = cartas[pos].Valor;
                    cartas[0].setSource(new Uri(this.BaseUri, @"Assets/Carta" + cartas[pos].Valor + ".jpg"));
                }
                else
                {
                    if (cartas[0].Valor == (cartas[pos].Valor - 1) && cartas[pos].Bandera == false)
                    {
                        cartas[pos].setSource(new Uri(this.BaseUri, @"Assets/Carta" + cartas[0].Valor + ".jpg"));
                        cartas[pos].Valor = cartas[0].Valor;
                        contadorJ--;
                        lbJugador.Text = "Restan: " + contadorJ.ToString();
                        Asignar(cartas[0].Pos);
                        return;
                    }
                    else if (cartas[0].Valor == (cartas[pos].Valor + 1) && cartas[pos].Bandera == true)
                    {
                        cartas[pos].setSource(new Uri(this.BaseUri, @"Assets/Carta" + cartas[0].Valor + ".jpg"));
                        cartas[pos].Valor = cartas[0].Valor;
                        contadorJ--;
                        lbJugador.Text = "Restan: " + contadorJ.ToString();
                        Asignar(cartas[0].Pos);
                        return;
                    }
                }
            }
        }

        void JuegaOponente()
        {
            if (contadorO == 1)
            {
                lbOponente.Text = "PERDISTE";
                lbJugador.Text = "PERDISTE";
                StoptTimers();
            }
            else
            {
                
                for (int i = 0; i < 4; i++)
                {
                    int j = 0;
                    for (int k = 5; k <= 8; k++)
                    {
                        int operador = 0;
                        if (boolBandera[j++])
                            operador = 1;
                        else
                            operador = -1;
                        if (varOponente[i] == (cartas[k].Valor + operador))
                        {
                            contadorO--;
                            lbOponente.Text = "Restan: " + contadorO;
                            cartas[k].Valor = varOponente[i];
                            cartas[k].setSource(new Uri(this.BaseUri, @"Assets/Carta" + varOponente[i] + ".jpg"));
                            varOponente[i] = r.Next(1, 14);
                            return;
                        }
                    }
                }

            }
        }

        public void timer1_Tick(object sender, object args)
        {
            JuegaOponente();
        }

        public void timer2_Tick(object sender, object args)
        {
            int[] flechas = new int[4];
            for (int i = 0; i < flechas.Length; i++)
            {
                flechas[i] = r.Next(0, 100);
                if (flechas[i] % 2 == 0)
                {
                    lbFlechas[i] .Text = "+";
                    cartas[(i+5)].Bandera = true;
                }
                else
                {
                    lbFlechas[i].Text = "-";
                    cartas[(i+5)].Bandera = false;
                }
            }
        }
        public void timer3_Tick(object sender, object args)
        {
            timer3Stop++;
            if (timer3Stop < 15)
            {
                if (lbF1.Visibility == Windows.UI.Xaml.Visibility.Visible)
                    lbF1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                else
                    lbF1.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else timer3.Stop();
        }

        private void timer4_Tick(object sender, object e)
        {
            RandPozo();
        }

    }
}
