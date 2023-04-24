using Microsoft.AspNetCore.Components.Forms;

namespace Client.App.ViewModels;

public class WebFileVm
{
    public IBrowserFile File { get; set; }

    public byte[] Buffers { get; set; }
    public string ImageUrl { get; set; }
}