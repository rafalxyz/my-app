import { List, Datagrid, TextField, TextInput, BooleanField, BooleanInput, DateInput, DateField } from 'react-admin';

const subscriptionFilters = [
    <TextInput source="email" label="Email" alwaysOn />,
    <DateInput source="dateFrom" label="Date From" alwaysOn />,
    <DateInput source="dateTo" label="Date To" alwaysOn />,
    <BooleanInput source="isActive" label="Active" alwaysOn />,
];

export const SubscriptionList = () => (
    <List filters={subscriptionFilters} sort={{ field: 'createdAt', order: 'DESC' }}>
        <Datagrid rowClick="show" bulkActionButtons={false}>
            <TextField source="id" />
            <TextField source="email" />
            <TextField source="productName" />
            <DateField source="createdAt" />
            <BooleanField source="isActive" />
        </Datagrid>
    </List>
);