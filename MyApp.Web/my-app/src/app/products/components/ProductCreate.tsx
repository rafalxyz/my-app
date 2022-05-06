import { Create } from 'react-admin'
import { ProductForm } from './ProductForm';

export const ProductCreate = (props: any) => (
    <Create {...props}>
        <ProductForm />
    </Create>
);