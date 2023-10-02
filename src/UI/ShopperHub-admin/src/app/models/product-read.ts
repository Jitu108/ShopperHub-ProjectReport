export class ProductRead {
    public id: number;
    public name: string;
    public description: string;
    public price: number;
    public mrp: number;
    public catalogTypeId: number;
    public catalogType: string;
    public catalogBrandId: number;
    public catalogBrand: string;
    public availableStock: number;
    public restockThreshold: number;
    public maxStockThreshold: number;
    public imageId: number;
    public imageName: string;
    public imageCaption: string;
    public toReplenish: boolean;
    public imageData: string | ArrayBuffer;
}