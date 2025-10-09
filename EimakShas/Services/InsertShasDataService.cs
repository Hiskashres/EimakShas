using EimakShas.Data;
using EimakShas.Interfaces;
using EimakShas.Migrations;
using EimakShas.Models;
using Microsoft.EntityFrameworkCore;

namespace EimakShas.Services
{
    public class LoadShasDataService : IInsertShasDataService
    {
        private readonly ApplicationDbContext _context;
        private List<ShasCycle> _shasCycles = new List<ShasCycle>();
        private List<Masechta> _masechtas = new List<Masechta>();
        private List<Daf> _dafim = new List<Daf>();
        private List<Umid> _umidim = new List<Umid>();


        public async Task InsertShasDataServiceAsync() 
        {
            //Earase all data
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();
            await _context.SaveChangesAsync();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Enter a Shas Cycle name: ");
            Console.ResetColor();
            string shasCycleName = Console.ReadLine();

            //await using var transaction = _context.Database.BeginTransactionAsync();
            await LoadShasCycleAsync(shasCycleName);
            await LoadMasechtasAsync();
            await LoadDafimAsync();
            await LoadUmidimAsync();
            //await _context.SaveChangesAsync();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Shas cycle \"{shasCycleName}\" loaded successfully!");
            Console.ResetColor();
            Console.ReadLine();
        }
        
        private async Task LoadShasCycleAsync(string shasCycleName)
        {
            _shasCycles.Add(new ShasCycle{ ShasCycleName = shasCycleName});
            await _context.AddRangeAsync( _shasCycles );
            await _context.SaveChangesAsync();
        }

        private async Task LoadMasechtasAsync() 
        {
            foreach (ShasCycle shasCycle in _shasCycles)
            {
                _masechtas.Add(new Masechta { ShasCycleId = shasCycle.ShasCycleId, MasechtaName = "ברכות", MasechtaDafCount = 63, MasechtaOrder = 1, LastUmidDoubleSided = false });
                _masechtas.Add(new Masechta { ShasCycleId = shasCycle.ShasCycleId, MasechtaName = "שבת", MasechtaDafCount = 156, MasechtaOrder = 2, LastUmidDoubleSided = true });
                _masechtas.Add(new Masechta { ShasCycleId = shasCycle.ShasCycleId, MasechtaName = "עירובין", MasechtaDafCount = 104, MasechtaOrder = 3, LastUmidDoubleSided = false });
                _masechtas.Add(new Masechta { ShasCycleId = shasCycle.ShasCycleId, MasechtaName = "פסחים", MasechtaDafCount = 120, MasechtaOrder = 4, LastUmidDoubleSided = true });
                _masechtas.Add(new Masechta { ShasCycleId = shasCycle.ShasCycleId, MasechtaName = "שקלים", MasechtaDafCount = 21, MasechtaOrder = 5, LastUmidDoubleSided = false });
                _masechtas.Add(new Masechta { ShasCycleId = shasCycle.ShasCycleId, MasechtaName = "ראש השנה", MasechtaDafCount = 34, MasechtaOrder = 6, LastUmidDoubleSided = false });
                _masechtas.Add(new Masechta { ShasCycleId = shasCycle.ShasCycleId, MasechtaName = "יומא", MasechtaDafCount = 87, MasechtaOrder = 7, LastUmidDoubleSided = false });
                _masechtas.Add(new Masechta { ShasCycleId = shasCycle.ShasCycleId, MasechtaName = "סוכה", MasechtaDafCount = 55, MasechtaOrder = 8, LastUmidDoubleSided = true });
                _masechtas.Add(new Masechta { ShasCycleId = shasCycle.ShasCycleId, MasechtaName = "ביצה", MasechtaDafCount = 39, MasechtaOrder = 9, LastUmidDoubleSided = true });
                _masechtas.Add(new Masechta { ShasCycleId = shasCycle.ShasCycleId, MasechtaName = "תענית", MasechtaDafCount = 30, MasechtaOrder = 10, LastUmidDoubleSided = false });
                _masechtas.Add(new Masechta { ShasCycleId = shasCycle.ShasCycleId, MasechtaName = "מגילה", MasechtaDafCount = 31, MasechtaOrder = 11, LastUmidDoubleSided = false });
                _masechtas.Add(new Masechta { ShasCycleId = shasCycle.ShasCycleId, MasechtaName = "מועד קטן", MasechtaDafCount = 28, MasechtaOrder = 13, LastUmidDoubleSided = false });
                _masechtas.Add(new Masechta { ShasCycleId = shasCycle.ShasCycleId, MasechtaName = "חגיגה", MasechtaDafCount = 26, MasechtaOrder = 14, LastUmidDoubleSided = false });
                // And so on...
            }
            await _context.AddRangeAsync(_masechtas);
            await _context.SaveChangesAsync();
        }

        private async Task LoadDafimAsync()
        { 
            foreach (Masechta masechta in _masechtas)
            {
                int dafimCount = 0;
                while (dafimCount < masechta.MasechtaDafCount)
                {
                    dafimCount++;
                    _dafim.Add(new Daf {MasechtaId = masechta.MasechtaId, DafLetter = ConvertDafLetter(dafimCount), IsCompleted = false });
                }
            }
            await _context.AddRangeAsync(_dafim);
            await _context.SaveChangesAsync();
        }

        private async Task LoadUmidimAsync()
        {
            foreach (Masechta masechta  in _masechtas)
            {
                if (masechta.LastUmidDoubleSided)
                {
                    foreach (Daf daf in _dafim.Where(daf => daf.MasechtaId == masechta.MasechtaId))
                    {
                        _umidim.Add(new Umid { DafId = daf.DafId, UserId = null, UmidLetter = "ע\"א", IsCompleted = false });
                        _umidim.Add(new Umid { DafId = daf.DafId, UserId = null, UmidLetter = "ע\"ב", IsCompleted = false });
                    }
                }
                else
                {
                    int dafimCount = 0;
                    foreach (Daf daf in _dafim.Where(daf => daf.MasechtaId == masechta.MasechtaId))
                    {
                        dafimCount++;
                        if (dafimCount < masechta.MasechtaDafCount)
                        {
                            _umidim.Add(new Umid { DafId = daf.DafId, UserId = null, UmidLetter = "ע\"א", IsCompleted = false });
                            _umidim.Add(new Umid { DafId = daf.DafId, UserId = null, UmidLetter = "ע\"ב", IsCompleted = false });
                        }
                        else
                        {
                            _umidim.Add(new Umid { DafId = daf.DafId, UserId = null, UmidLetter = "ע\"א", IsCompleted = false });
                        }
                    }
                }
            }
            await _context.AddRangeAsync(_umidim);
            await _context.SaveChangesAsync();
        }
        public LoadShasDataService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        private string ConvertDafLetter(int inputNum)
        {
            int calculatingNum;
            int hundreds;
            int tens;
            int ones;
            int specialChar;
            string outputLetter = "";
            var numberssAndLetters = new Dictionary<int, string>
            {
                { 0, "" }, { 1, "א" }, { 2, "ב" }, { 3, "ג" }, { 4, "ד" },
                { 5, "ה" }, { 6, "ו" }, { 7, "ז" }, { 8, "ח" }, { 9, "ט" },
                { 10, "י"}, { 20, "כ" }, { 30, "ל" }, { 40, "מ" }, { 50, "נ" },
                { 60, "ס" }, { 70, "ע" }, { 80, "פ" }, { 90, "צ" }, { 100, "ק" },
                { 200, "ר" }, { 300, "ש" }, { 400, "ת" }, { 500, "תק" }, { 600, "תר" },
                { 700, "תש" }, { 800, "תת" }, { 900, "תתק" }
            };

            inputNum++;
            calculatingNum = inputNum;
            hundreds = calculatingNum / 100;
            hundreds = hundreds * 100;
            calculatingNum = calculatingNum - hundreds;
            specialChar = calculatingNum;
            tens = calculatingNum / 10;
            tens = tens * 10;
            calculatingNum = calculatingNum - tens;
            ones = calculatingNum;

            string onesTranslated = numberssAndLetters[ones];
            string tensTranslated = numberssAndLetters[tens];
            string hundredsTranslated = numberssAndLetters[hundreds];

            if (specialChar == 15)
            {
                tensTranslated = "טו";
                onesTranslated = "";
            }
            else if (specialChar == 16)
            {
                tensTranslated = "טז";
                onesTranslated = "";
            }

            return outputLetter = hundredsTranslated + tensTranslated + onesTranslated;
        }
    }
}
