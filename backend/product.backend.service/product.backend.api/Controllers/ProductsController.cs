using Microsoft.AspNetCore.Mvc;
using product.backend.application.Products;
using product.backend.application.Products.Commands.Create;
using product.backend.domain.Products.Domain;
using product.backend.domain.Products.DTO;
using product.backend.shared;
using System;

namespace product.backend.api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductApp _productApp;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ProductApp productApp, IWebHostEnvironment environment, ILogger<ProductsController> logger)
        {
            _productApp = productApp;
            _hostingEnvironment = environment;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> List()
        {
            StatusResponse<IEnumerable<ResponseProduct>> status = await this._productApp.List();

            if (!status.Success)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] CreateProductCommand product)
        {

            StatusResponse<Product> status = await this._productApp.Create(product);
            if (!status.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, status);
            }

            return StatusCode(StatusCodes.Status201Created, status.Data);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] Product product)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return Ok();
        }


        [HttpPost]
        [Route("{id}/foto")]
        public async Task<ActionResult> UploadPhoto([FromRoute] int id, IFormFile file)
        {
            string uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "Uploadsx");

            if (file.Length > 0)
            {
                string filePath = Path.Combine(uploads, file.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    catch (Exception ex)
                    {
                        this._logger.LogError(ex, "archivo: {0}", file.FileName);
                        return StatusCode(StatusCodes.Status500InternalServerError, ex);
                    }
                }
            }

            return Ok();
        }

        [HttpPost]
        [Route("{id}/fotos")]
        public async Task<ActionResult> UploadPhoto([FromRoute] int id, IList<IFormFile> files)
        {
            StatusResponseSimple respuesta = new StatusResponseSimple(true, "");

            string uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
            foreach (IFormFile file in files)
            {
                if (file.Length > 0)
                {
                    string filePath = Path.Combine(uploads, file.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        try
                        {
                            throw new Exception("Error generado intencionalmente");
                            await file.CopyToAsync(fileStream);
                        }
                        catch (Exception ex)
                        {
                            this._logger.LogError(ex, "No se pudo guardar el archivo {0}. Id : {1}", file.FileName, respuesta.TraceId);

                            respuesta.Success = false;
                            respuesta.Title = string.Format("No se pudo guardar el archivo {0}", file.FileName); ;
                            respuesta.Detail = ex.ToString();
                            return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                        }
                    }
                }
            }
            return Ok();
        }

        //[HttpPost]
        //[Route("product-multimedia")]
        //public async Task<ActionResult> UploadPhotoAndData([FromRoute] int id, [FromForm] ProductMultimedia productMultimedia)
        //{
        //    string uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
        //    Product product = new Product();
        //    product.Id = productMultimedia.Id;
        //    product.Title = productMultimedia.Title;

        //    //foreach (IFormFile file in productMultimedia.Images)
        //    //{
        //    //    if (file.Length > 0)
        //    //    {
        //    //        string filePath = Path.Combine(uploads, file.FileName);
        //    //        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
        //    //        {
        //    //            await file.CopyToAsync(fileStream);
        //    //        }
        //    //    }
        //    //}
        //    return Ok();
        //}
    }
}