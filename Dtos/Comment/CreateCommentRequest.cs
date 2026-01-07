using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace finshark_api.Dtos.Comment
{
    public class CreateCommentRequest
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must not be less than 5 characters.")]
        [MaxLength(100, ErrorMessage = "Title must not exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must not be less than 5 characters.")]
        [MaxLength(100, ErrorMessage = "Content must not exceed 100 characters.")]
        public string Content { get; set; } = string.Empty;

    }
}