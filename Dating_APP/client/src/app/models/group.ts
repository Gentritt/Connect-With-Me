export interface Group{
    name: string;
    connections:Connection[];
}

interface Connection{
    connctionId: string;
    username: string;
}