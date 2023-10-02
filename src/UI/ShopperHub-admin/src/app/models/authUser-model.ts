export type AuthUserModel = {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    roleId: number;
    role: string;
    fullName: string;
    token: string;
    expiryDate: Date
}