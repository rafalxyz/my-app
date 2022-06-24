import { useRecordContext } from 'react-admin';

const formatter = new Intl.NumberFormat('en-US', {
  style: 'currency',
  currency: 'USD',
  minimumFractionDigits: 2,
  maximumFractionDigits: 2,
});

const PriceField = ({ source }: { source: string }) => {
  const record = useRecordContext();
  return record ? <span>{formatter.format(record[source])}</span> : null;
};

export default PriceField;
