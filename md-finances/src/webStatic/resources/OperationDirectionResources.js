import Direction from '@moedelo/frontend-enums/mdEnums/Direction';

const operationDirectionResource = [
    {
        value: Direction.Default,
        text: `Списания и поступления`
    },
    {
        value: Direction.Outgoing,
        text: `Списания`
    },
    {
        value: Direction.Incoming,
        text: `Поступления`
    }
];

export default operationDirectionResource;
