using System.ComponentModel.DataAnnotations;

public enum SizeOptions
{
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
}