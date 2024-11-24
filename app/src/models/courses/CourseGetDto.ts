import { PdfGetDto } from "../pdfs/PdfGetDto";
import { ReviewGetDto } from "../reviews/ReviewGetDto";
import { VideoGetDto } from "../videos/VideoGetDto";

export interface CourseGetDto {
    courseId:       number;
    name:           string;
    description:    string;
    modifiedAt:     string;
    createdAt:      string;
    status:         boolean;
    imgPath:        string;
    pdfs:           PdfGetDto[];
    reviews:        ReviewGetDto[];
    videos:         VideoGetDto[];
  }