import { Admin, Resource } from 'react-admin';
import { ProductList } from './app/products/components/ProductList';
import { ProductEdit } from './app/products/components/ProductEdit';
import { ProductCreate } from './app/products/components/ProductCreate';
import { ProductShow } from './app/products/components/ProductShow';
import PostIcon from '@mui/icons-material/Book';
import NotificationsActive from '@mui/icons-material/NotificationsActive';
import dataProvider from './app/shared/react-admin/dataProvider';
import { theme } from './theme';
import usePriceSubscriptions from './app/products/hooks/usePriceSubscription';
import { useEffect } from 'react';
import { getSubscriptionsFromLocalStorage } from './app/products/utils';
import { SubscriptionList } from './app/subscriptions/components/SubscriptionList';

const App = () => {
  const { setSubscriptions } = usePriceSubscriptions();

  useEffect(() => {
    setSubscriptions(getSubscriptionsFromLocalStorage());
  }, [setSubscriptions]);

  return (
    <Admin dataProvider={dataProvider} theme={theme}>
      <Resource name="products" list={ProductList} show={ProductShow} edit={ProductEdit} create={ProductCreate} icon={PostIcon} />
      <Resource name="subscriptions" list={SubscriptionList} icon={NotificationsActive} />
    </Admin>
);
}

export default App;
