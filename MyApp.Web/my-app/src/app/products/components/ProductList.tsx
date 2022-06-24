import {
  List,
  Datagrid,
  TextField,
  EditButton,
  TextInput,
  NumberField,
  BooleanField,
  ImageField,
} from 'react-admin';
import PriceField from '../../shared/react-admin/fields/PriceField';

const productFilters = [<TextInput source="name" label="Search" alwaysOn />];

export const ProductList = () => (
  <List filters={productFilters} sort={{ field: 'name', order: 'ASC' }}>
    <Datagrid rowClick="show">
      <ImageField source="imageUrl" label="Image" sortable={false} />
      <TextField source="id" />
      <TextField source="name" />
      <PriceField source="price" />
      <NumberField source="quantity" />
      <BooleanField source="isActive" />
      <EditButton />
    </Datagrid>
  </List>
);
