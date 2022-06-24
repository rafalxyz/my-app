import {
  Show,
  SimpleShowLayout,
  TextField,
  NumberField,
  BooleanField,
  useShowContext,
  ImageField,
} from 'react-admin';
import usePriceSubscriptions from '../hooks/usePriceSubscription';
import { ProductPriceSubscribeButton } from './ProductPriceSubscribeButton';
import { ProductPriceUnsubscribeButton } from './ProductPriceUnsubscribeButton';
import PriceField from '../../shared/react-admin/fields/PriceField';
import Box from '@mui/material/Box';

const ProductShowLayoutInternal = ({ record }: { record: any }) => {
  const { subscriptions } = usePriceSubscriptions();

  const subscriptionId = subscriptions.find(
    (x) => x.productId === record.id
  )?.id;

  return (
    <SimpleShowLayout>
      <TextField source="name" />
      <ImageField source="imageUrl" label="Image" />
      <>
        <PriceField source="price" />
        <Box marginLeft={2} display="inline">
          {subscriptionId ? (
            <ProductPriceUnsubscribeButton subscriptionId={subscriptionId} />
          ) : (
            <ProductPriceSubscribeButton productId={record.id} />
          )}
        </Box>
      </>
      <NumberField source="description" />
      <NumberField source="quantity" />
      <BooleanField source="isActive" />
    </SimpleShowLayout>
  );
};

const ProductShowLayout = () => {
  const { isLoading, record } = useShowContext();

  if (isLoading || !record) {
    return <div>Loading...</div>;
  }

  return <ProductShowLayoutInternal record={record} />;
};

export const ProductShow = () => (
  <Show>
    <ProductShowLayout />
  </Show>
);
