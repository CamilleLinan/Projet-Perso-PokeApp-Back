export interface Pokemon {
    id: string,
    names: {
        fr: string,
        en: string,
        ja: string,
    },
    types: string[],
    sprite: string,
}