using System.ComponentModel.DataAnnotations;

namespace ProductService.Enums
{
    public enum SizeOptions
    {
        [Display(Name = "2XS")]
        XXS,
        XS,
        S,
        M,
        L,
        XL,
        XXL,
        [Display(Name = "3XL")]
        XXXL,
        [Display(Name = "4XL")]
        XXXXL,
        [Display(Name = "One Size")]
        OneSize
    }
}
