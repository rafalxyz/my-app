import { TextInput, NumberInput, BooleanInput, SimpleForm, required, minLength, maxLength, number, regex, minValue, ImageInput, ImageField } from "react-admin";
import { Grid, InputAdornment } from "@mui/material";

const validateName = [
    required('Please provide name'),
    minLength(2, 'Name must be at least 2 characters long'),
    maxLength(100, 'Name must be at most 100 characters long')
];

const validatePrice = [
    required('Please provide price'),
    regex(/^\d+(,\d{1,2})?$/, 'Please provide a valid price')
];

const validateQuantity = [
    required('Please provide quantity'),
    number('Please provide a VALID quantity'),
    minValue(0, 'Quantity cannot be negative'),
];

const validateDescription = [
    required('Please provide description'),
    maxLength(10000, 'Description must be at most 10000 characters long')
];

export const ProductForm = ({ isEdit }: { isEdit?: boolean }) => (
  <SimpleForm>
    <Grid container rowSpacing={1} columnSpacing={2}>
      {isEdit ? (
        <>
          <Grid item xs={3}>
            <TextInput disabled source="id" fullWidth />
          </Grid>
          <Grid item xs={9}></Grid>
        </>
      ) : null}
      <Grid item xs={6}>
        <TextInput source="name" validate={validateName} fullWidth />
      </Grid>
      <Grid item xs={6}></Grid>
      <Grid item xs={2}>
        <NumberInput source="price" validate={validatePrice} InputProps={{
            startAdornment: <InputAdornment position="start">$</InputAdornment>,
          }} />
      </Grid>
      <Grid item xs={12}>
        <NumberInput source="quantity" validate={validateQuantity} />
      </Grid>
      <Grid item xs={12}>
        <TextInput multiline source="description" fullWidth validate={validateDescription} />
      </Grid>
      <Grid item xs={12}>
        <ImageInput source="image" label="Image" accept="image/*" multiple={false}>
          <ImageField source="src" title="title" />
        </ImageInput>
      </Grid>
      <Grid item xs={12}>
        <BooleanInput source="isActive" />
      </Grid>
    </Grid>
  </SimpleForm>
);
