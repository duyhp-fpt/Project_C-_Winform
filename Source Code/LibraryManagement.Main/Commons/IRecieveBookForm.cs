namespace LibraryManagement.Commons {
    public interface IRecieveBookForm: ISimpleForm {
        string StudentEmail { get; set; }
        string BookID { get; set; }
    }
}
