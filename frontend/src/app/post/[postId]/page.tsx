import PostForm from "@/src/components/post/PostForm";

interface PageProps {
    params: Promise<{
        postId: string
    }>
}

export default async function PostPage({ params }: PageProps) {
    const { postId } = await params

    return (
        <div>
            <h1>Post {postId}</h1>
        </div>
    )
}
