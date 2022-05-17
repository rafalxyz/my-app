import { DataProvider, fetchUtils } from 'react-admin';
import { stringify } from 'query-string';

const apiUrl = process.env.REACT_APP_API_BASE_URL;
const httpClient = fetchUtils.fetchJson;

const dataProvider: DataProvider = {
    getList: (resource, params) => {
        const { page, perPage } = params.pagination;
        const { field, order } = params.sort;

        console.log(apiUrl);

        const query = {
            pageNumber: page,
            pageSize: perPage,
            orderBy: field,
            orderDirection: order,
            ...params.filter
        };
        const url = `${apiUrl}/${resource}/search?${stringify(query)}`;

        return httpClient(url).then(({ json }) => ({
            data: json.items,
            total: json.totalCount
        }));
    },

    getOne: (resource, params) =>
        httpClient(`${apiUrl}/${resource}/${params.id}`).then(({ json }) => ({
            data: json,
        })),

    getMany: (resource, params) => {
        const query = {
            filter: JSON.stringify({ id: params.ids }),
        };
        const url = `${apiUrl}/${resource}?${stringify(query)}`;
        return httpClient(url).then(({ json }) => ({ data: json }));
    },

    getManyReference: (resource, params) => {
        const { page, perPage } = params.pagination;
        const { field, order } = params.sort;
        const query = {
            sort: JSON.stringify([field, order]),
            range: JSON.stringify([(page - 1) * perPage, page * perPage - 1]),
            filter: JSON.stringify({
                ...params.filter,
                [params.target]: params.id,
            }),
        };
        const url = `${apiUrl}/${resource}?${stringify(query)}`;

        return httpClient(url).then(({ json }) => ({
            data: json,
            total: json.totalCount
        }));
    },

    update: (resource, params) => {
        const body = resource === 'products' ? getProductFormData(params.data) : JSON.stringify(params.data);

        return httpClient(`${apiUrl}/${resource}/${params.id}`, {
            method: 'PUT',
            body
        }).then(({ json }) => ({ data: json }));
    },

    updateMany: (resource, params) => {
        const query = {
            filter: JSON.stringify({ id: params.ids}),
        };
        return httpClient(`${apiUrl}/${resource}?${stringify(query)}`, {
            method: 'PUT',
            body: JSON.stringify(params.data),
        }).then(({ json }) => ({ data: json }));
    },

    create: (resource, params) => {
        const body = resource === 'products' ? getProductFormData(params.data) : JSON.stringify(params.data);

        return httpClient(`${apiUrl}/${resource}`, {
            method: 'POST',
            body,
        }).then(({ json }) => ({
            data: { ...params.data, id: json.id },
        }));
    },
    delete: (resource, params) =>
        httpClient(`${apiUrl}/${resource}/${params.id}`, {
            method: 'DELETE',
        }).then(({ json }) => ({ data: json })),

    deleteMany: (resource, params) => {
        const query = {
            ids: params.ids
        };
        return httpClient(`${apiUrl}/${resource}?${stringify(query)}`, {
            method: 'DELETE',
        }).then(({ json }) => ({ data: [] }));
    }
};

// TODO: Make it better
const getProductFormData = (data: any) => {
    const formData = new FormData();

    formData.append('name', data.name);
    formData.append('isActive', data.isActive);
    formData.append('quantity', data.quantity);
    formData.append('price', data.price);
    formData.append('description', data.description);
    formData.append('image', data.image.rawFile);

    return formData;
}

export default dataProvider;