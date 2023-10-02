export class ProductCreate {
    public id: number;
    public name: string;
    public description: string;
    public price: number;
    public mrp: number;
    public catalogTypeId: number;
    public catalogBrandId: number;
    public availableStock: number;
    public restockThreshold: number;
    public maxStockThreshold: number;
    public imageId: number;
    public imageName: string;
    public imageCaption: string;
    public imageData: string | ArrayBuffer;
}