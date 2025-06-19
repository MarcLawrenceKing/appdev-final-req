using Microsoft.AspNetCore.Mvc;
using appdev_final_req.Models.Entitiess;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using appdev_final_req.Data;

public class MembersController : Controller
{
    private readonly ApplicationDbContext _context;

    public MembersController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult List(string search)
    {
        var members = string.IsNullOrWhiteSpace(search)
            ? _context.Members.ToList()
            : _context.Members
                      .Where(m => m.FullName.Contains(search) || m.Email.Contains(search) || m.Phone.Contains(search))
                      .ToList();

        return View(members);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Member member)
    {
        if (ModelState.IsValid)
        {
            _context.Members.Add(member);
            _context.SaveChanges();
            TempData["Message"] = "Member added successfully!";
            return RedirectToAction("List");
        }

        return View(member);
    }

    public IActionResult Edit(int id)
    {
        var member = _context.Members.FirstOrDefault(m => m.Id == id);
        if (member == null) return NotFound();
        return View(member);
    }

    [HttpPost]
    public IActionResult Edit(Member member)
    {
        if (ModelState.IsValid)
        {
            _context.Members.Update(member);
            _context.SaveChanges();
            TempData["Message"] = "Member updated successfully!";
            return RedirectToAction("List");
        }

        return View(member);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var member = _context.Members.FirstOrDefault(m => m.Id == id);
        if (member != null)
        {
            _context.Members.Remove(member);
            _context.SaveChanges();
            TempData["Message"] = "Member deleted!";
        }

        return RedirectToAction("List");
    }

    [HttpPost]
    public IActionResult DeleteSelected(string ids)
    {
        if (!string.IsNullOrEmpty(ids))
        {
            var idList = ids.Split(',').Select(int.Parse).ToList();
            var members = _context.Members.Where(m => idList.Contains(m.Id)).ToList();
            _context.Members.RemoveRange(members);
            _context.SaveChanges();
            TempData["Message"] = $"{members.Count} members deleted.";
        }

        return RedirectToAction("List");
    }

    [HttpPost]
    public async Task<IActionResult> BatchUpload(IFormFile file)
    {
        try
        {
            if (file != null && file.Length > 0)
            {
                using var reader = new StreamReader(file.OpenReadStream());

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    PrepareHeaderForMatch = args => args.Header.ToLower(),
                    HeaderValidated = null,
                    MissingFieldFound = null,
                    BadDataFound = null
                };

                using var csv = new CsvReader(reader, config);
                csv.Context.TypeConverterCache.AddConverter<DateOnly>(new DateOnlyConverter());

                var members = csv.GetRecords<Member>().ToList();
                _context.Members.AddRange(members);
                await _context.SaveChangesAsync();

                TempData["UploadMessage"] = $"{members.Count} members uploaded successfully!";
                return RedirectToAction("List");
            }

            TempData["UploadMessage"] = "Please upload a valid CSV file.";
            return RedirectToAction("List");
        }
        catch (Exception ex)
        {
            TempData["UploadMessage"] = "Error uploading file: " + ex.Message;
            return RedirectToAction("List");
        }
    }
}
