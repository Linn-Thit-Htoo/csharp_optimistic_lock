using csharp_optimistic_lock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<AppDbContext>()
                .BuildServiceProvider();

            var dbContext = serviceProvider.GetService<AppDbContext>();

            BlogDbService blogDbService = new(dbContext);
            //await blogDbService.AddAsync(new AddBlogRequestModel()
            //{
            //    BlogTitle = "Blog Title 1",
            //    BlogAuthor = "Blog Author 1",
            //    BlogContent = "Blog Content"
            //});

            await blogDbService.UpdateAsync(new UpdateBlogRequestModel()
            {
                BlogId = "06c3b0c9-4ad6-4ae0-9607-f552c87a5ef4",
                BlogTitle = "Blog Title 1 edited",
                BlogAuthor = "Blog Author 1 edited",
                BlogContent = "Blog Content 1 edited"
            });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

public class BlogDbService
{
    private readonly AppDbContext _context;

    public BlogDbService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(AddBlogRequestModel requestModel)
    {
        try
        {
            await _context.TblBlogs.AddAsync(new Tbl_Blog()
            {
                BlogId = Guid.NewGuid().ToString(),
                BlogTitle = requestModel.BlogTitle,
                BlogAuthor = requestModel.BlogAuthor,
                BlogContent = requestModel.BlogContent,
            });
            await _context.SaveChangesAsync();

            Console.WriteLine("Saving Successful.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateAsync(UpdateBlogRequestModel requestModel)
    {
        try
        {
            var item = await _context.TblBlogs
                .FindAsync(requestModel.BlogId) ?? throw new Exception("No data found.");

            item.BlogTitle = requestModel.BlogTitle;
            item.BlogAuthor = requestModel.BlogAuthor;
            item.BlogContent = requestModel.BlogContent;

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            Console.WriteLine("Updating Successful.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
