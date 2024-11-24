export interface FaqGetDto {
    faqId: number;
    question: string;
    answer: string;
    modifiedAt: string;
    createdAt: string;
    status: boolean;
}