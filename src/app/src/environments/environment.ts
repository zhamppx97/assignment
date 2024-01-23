export const environment = {
    production: false,
    version: "0.0.1",
    apiService: {
        host: 'https://localhost:44392'
    },
    apiEndpoint: {
        Users: {
            Select: '/users/select',
            Insert: '/users/insert',
            Update: '/users/update',
            Delete: '/users/delete',
        },
    }
};
