import PostForm from "@/src/components/post/PostForm";

interface PageProps {
    params: Promise<{
        profileId: string
    }>
}

export default async function ProfilePage({ params }: PageProps) {
    const { profileId } = await params

    return (
        <div>
            <h1>Profile {profileId}</h1>
        </div>
    )
}
