namespace BloggingApp.Data.Entities
{
	public class PostDto
    {
		public int Id { get; set; }
		public int BlogId { get; set; }
		public string Content { get; set; }
		public string Title { get; set; }
	}
}
