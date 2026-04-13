namespace Moedelo.Common.Enums.Enums.Consultations
{
    //Своего рода права, регулируют в какую вкладку партнёрки попадут консультации пользователя. Вычисляются по тарифному праву.
    public enum ConsultationLevel
    {
        Biz = 1,
        Pro = 2,
        Outsource = 3,
        Usn = 4,
        FinguruOutsource = 5,
        KnopkaOutsource = 6,
        Acc = 7,
        Osno = 8,
        ReadOnly = 99
    }
}