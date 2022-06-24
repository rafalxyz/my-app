import { Edit, useRecordContext } from 'react-admin';

import { ProductForm } from './ProductForm';

const ProductTitle = () => {
  const record = useRecordContext();
  return <span>Product {record ? `"${record.name}"` : ''}</span>;
};

export const ProductEdit = () => (
  <Edit title={<ProductTitle />}>
    <ProductForm isEdit />
  </Edit>
);
