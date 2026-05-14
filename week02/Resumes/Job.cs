public class Job
{
    // Member variables (fields) – begin with underscore and lowercase
    private string _company;
    private string _jobTitle;
    private int _startYear;
    private int _endYear;

    // Constructor (optional but helpful)
    public Job(string company, string jobTitle, int startYear, int endYear)
    {
        _company = company;
        _jobTitle = jobTitle;
        _startYear = startYear;
        _endYear = endYear;
    }

    // Display method – prints the job in the required format
    public void Display()
    {
        Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
    }
}