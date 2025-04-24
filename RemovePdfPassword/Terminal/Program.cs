using CommandLine;
using CommandLine.Text;
using iText.Forms.Form.Element;
using RemovePdfPassword;

CommandLine.Parser.Default.ParseArguments<Options>(args)
    .WithParsed(RunOptions)
    .WithNotParsed(HandleParseError);

static void RunOptions(Options options)
{
    //if (!string.IsNullOrWhiteSpace(options.filename))
    //{
    //    Console.WriteLine($"We are going to work on file '{options.filename}'");
    //}

    //if (!string.IsNullOrWhiteSpace(options.password))
    //{
    //    Console.WriteLine($"We are going to work with password '{options.password}'");
    //}

    Console.WriteLine($"About to generate output file: '{options.outputfile}'");

    if (string.IsNullOrWhiteSpace(options.outputfile))
    {
        options.outputfile = Path.Combine(
            Path.GetDirectoryName(options.filename)!,
            Path.GetFileNameWithoutExtension(options.filename) + ".unprotect.pdf");
    }
    Console.WriteLine($"About to generate output file: '{options.outputfile}'");

    var removePdfPasswordService = new RemovePdfPasswordService();
    removePdfPasswordService.Execute(options.filename, options.password, options.outputfile);
}
static void HandleParseError(IEnumerable<Error> errs)
{
    foreach (var error in errs)
    {
        Console.WriteLine(error.ToString());
    }
}

public class Options
{
    [Option("filename", Required = true, HelpText = "Input filename to remove password.")]
    public required string filename { get; set; }
    
    [Option("outputfile", Required = false, HelpText = "outputfile filename without password.")]
    public required string outputfile { get; set; }

    [Option("password", Required = true, HelpText = "The password for the file.")]
    public required string password { get; set; }

    [Usage(ApplicationAlias = "RemovePdfPassword")]
    public static IEnumerable<Example> Examples
    {
        get
        {
            return new List<Example>() {
                new Example("Normal scenario", new Options { filename = "pdfWithPassword.pdf", password = "myPassword", outputfile = "pdfWithoutPassword" }),
                new Example("Changing output file name", new Options { filename = "pdfWithPassword.pdf", password = "myPassword", outputfile = "pdfWithoutPassword" }),
                //new Example("  RemovePdfPassword --filename inputfile.pdf --password myPassword", new Options { filename = "file.pdf", password = "myPassword"}),
            };
        }
    }
}