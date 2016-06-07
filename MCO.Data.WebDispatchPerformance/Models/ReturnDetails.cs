namespace MCO.Data.WebDispatchPerformance.Models
{
    public class ReturnDetails
    {
        public string RETURN_DAY { get; set; }
        public string DAYOFWEEK { get; set; }
        public string START_HOUR { get; set; }
        public string RETURNED_BY { get; set; }
        public int NUMBER_OF_ORDERS { get; set; }
        public int QTY_RETURNED { get; set; }
        public int QTY_FIT { get; set; }
        public int QTY_GALA { get; set; }
        public int QTY_WRITEOFF { get; set; }
        public int QTY_ATTENTN { get; set; }
    }
}
