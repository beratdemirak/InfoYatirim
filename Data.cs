namespace InfoYatirim.Consumer
{
    using System;

    public class Data
    {
        public DateTime T { get; set; } // Veri oluşturulma zamanı
        public double O { get; set; }   // Açılış değeri
        public double H { get; set; }   // Yüksek değer
        public double L { get; set; }   // Düşük değer
        public double C { get; set; }   // Kapanış değeri
        public decimal V { get; set; }  // Hacim
    }

}
